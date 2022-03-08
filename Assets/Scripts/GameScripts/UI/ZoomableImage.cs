using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameScripts.UI
{
    [RequireComponent(typeof(AspectRatioFitter))]
    public class ZoomableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private RectTransform _content;
        [SerializeField] private float _zoomSensitivity = 0.0001f;
        [SerializeField] private float _minZoom = 1f;
        [SerializeField] private float _maxZoom = 2f;

        private Image _contentImage;
        private readonly List<PointerEventData> _touches = new List<PointerEventData>();
        private bool _isDrag;
        private bool _isZoom;
        private Bounds _viewBounds;
        private Bounds _contentBounds;
        private Vector2 _pointerStartLocalCursor;
        private Vector2 _contentStartPosition;
        private readonly Vector3[] _corners = new Vector3[4];
        private float _startPinchDistance;
        private float _startZoom;
        private Vector2 _pivot;
        private AspectRatioFitter _aspectSizeFitter;

        public event Action PlaceClicked = () => { };
        public event Action<Vector2> PositionChanged = p => { };
        public event Action<float> ZoomChanged = z => { };

        public float Zoom { get; private set; }
        public Vector2 ContentPosition => _content.anchoredPosition;

        private void Awake()
        {
            _aspectSizeFitter = GetComponent<AspectRatioFitter>();
            _contentImage = _content.GetComponent<Image>();

            Zoom = 1f;
            _content.localScale = Vector3.one;

            UpdateContentBounds();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_touches.Count >= 2)
            {
                return;
            }

            if (_touches.All(t => t.pointerId != eventData.pointerId))
            {
                _touches.Add(eventData);
            }

            if (_touches.Count == 1)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, _touches[0].position, _touches[0].pressEventCamera, out _pointerStartLocalCursor);
                _contentStartPosition = _content.anchoredPosition;
            }

            if (_touches.Count == 2)
            {
                var middlePosition = (_touches[0].position + _touches[1].position) / 2f;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, middlePosition, _touches[0].pressEventCamera, out _pointerStartLocalCursor);
                _contentStartPosition = _content.anchoredPosition;

                _startZoom = Zoom;
                _startPinchDistance = Vector2.Distance(_touches[0].position, _touches[1].position);
                UpdatePivot(_pointerStartLocalCursor);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var touch = _touches.FirstOrDefault(t => t.pointerId == eventData.pointerId);
            if (touch != null)
            {
                _touches.Remove(touch);

                if (!_isZoom && !_isDrag)
                {
                    PlaceClicked();
                }
                else if (_touches.Count == 1)
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, _touches[0].position, _touches[0].pressEventCamera, out _pointerStartLocalCursor);
                    _contentStartPosition = _content.anchoredPosition;
                    _isZoom = false;
                }
                else
                {
                    _isDrag = false;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDrag = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_touches.Count == 1)
            {
                if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, _touches[0].position, _touches[0].pressEventCamera, out var localCursor))
                {
                    return;
                }
                var prevPosition = _content.anchoredPosition;
                var pointerDelta = localCursor - _pointerStartLocalCursor;
                var position = _contentStartPosition + pointerDelta;
                _content.anchoredPosition = position;

                ClampContentPosition();

                if ((prevPosition - _content.anchoredPosition).sqrMagnitude > float.Epsilon)
                {
                    PositionChanged(_content.anchoredPosition);
                }
            }
            else if (_touches.Count == 2)
            {
                //Moving
                var prevPosition = _content.anchoredPosition;
                var middlePoint = (_touches[0].position + _touches[1].position) / 2f;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, middlePoint, _touches[0].pressEventCamera, out var localCursor);
                var pointerDelta = localCursor - _pointerStartLocalCursor;
                var position = _contentStartPosition + pointerDelta;
                _content.anchoredPosition = position;

                //Zooming
                _content.pivot = _pivot;

                var deltaDistance = Vector3.Distance(_touches[0].position, _touches[1].position) - _startPinchDistance;
                print("Delta distance " + deltaDistance);
                var prevZoom = Zoom;
                Zoom = _startZoom + deltaDistance * _zoomSensitivity;
                Zoom = Mathf.Clamp(Zoom, _minZoom, _maxZoom);
                _content.localScale = Zoom * Vector3.one;
                _content.pivot = new Vector2(0.5f, 0.5f);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(_view, middlePoint, _touches[0].pressEventCamera, out localCursor);
                UpdatePivot(localCursor);

                ClampContentPosition();

                if ((prevPosition - _content.anchoredPosition).sqrMagnitude > float.Epsilon)
                {
                    PositionChanged(_content.anchoredPosition);
                }

                if (Mathf.Abs(Zoom - prevZoom) > float.Epsilon)
                {
                    ZoomChanged(Zoom);
                }
            }
        }

        private void ClampContentPosition()
        {
            UpdateContentBounds();

            var delta = Vector2.zero;
            if (_viewBounds.max.x > _contentBounds.max.x)
            {
                delta.x = Math.Min(_viewBounds.min.x - _contentBounds.min.x, _viewBounds.max.x - _contentBounds.max.x);
            }
            else if (_viewBounds.min.x < _contentBounds.min.x)
            {
                delta.x = Math.Max(_viewBounds.min.x - _contentBounds.min.x, _viewBounds.max.x - _contentBounds.max.x);
            }

            if (_viewBounds.min.y < _contentBounds.min.y)
            {
                delta.y = Math.Max(_viewBounds.min.y - _contentBounds.min.y, _viewBounds.max.y - _contentBounds.max.y);
            }
            else if (_viewBounds.max.y > _contentBounds.max.y)
            {
                delta.y = Math.Min(_viewBounds.min.y - _contentBounds.min.y, _viewBounds.max.y - _contentBounds.max.y);
            }

            _content.anchoredPosition += delta;
        }
        private void UpdateContentBounds()
        {
            var rect = _view.rect;
            _viewBounds = new Bounds(rect.center, rect.size);
            _contentBounds = GetBounds();
        }

        private Bounds GetBounds()
        {
            if (_content == null)
            {
                return new Bounds();
            }

            _content.GetWorldCorners(_corners);
            var viewWorldToLocalMatrix = _view.worldToLocalMatrix;
            return InternalGetBounds(_corners, ref viewWorldToLocalMatrix);
        }

        private static Bounds InternalGetBounds(Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
        {
            var vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            for (var j = 0; j < 4; j++)
            {
                var v = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[j]);
                vMin = Vector3.Min(v, vMin);
                vMax = Vector3.Max(v, vMax);
            }

            var bounds = new Bounds(vMin, Vector3.zero);
            bounds.Encapsulate(vMax);
            return bounds;
        }

        private void UpdatePivot(Vector2 localCursor)
        {
            var x = _content.pivot.x + (localCursor.x / _content.rect.width);
            var y = _content.pivot.y + (localCursor.y / _content.rect.height);
            _pivot = new Vector2(x, y);
        }

        public void InitSize(bool withControlsHeight, Vector2 imageSize)
        {
            _aspectSizeFitter.aspectMode =
                withControlsHeight ? AspectRatioFitter.AspectMode.WidthControlsHeight : AspectRatioFitter.AspectMode.HeightControlsWidth;
            _content.sizeDelta = imageSize;
        }

        public void SetImage(Sprite image)
        {
            _contentImage.sprite = image;
        }

        public void SetPositionWithoutNotification(Vector2 anchoredPosition)
        {
            _content.anchoredPosition = anchoredPosition;
        }

        public void SetZoomWithoutNotification(float zoom)
        {
            Zoom = zoom;
            _content.localScale = Zoom * Vector3.one;
        }

        /*
        [SerializeField]
        private bool _inertia;
        [SerializeField]
        private float _decelerationRate = 0.135f;

        private Vector2 _velocity;
        private Vector2 _prevPosition;

        private void LateUpdate()
        {
            var deltaTime = Time.unscaledDeltaTime;
            var offset = CalculateOffset(Vector2.zero);
            if (!_isDrag && (offset != Vector2.zero || _velocity != Vector2.zero))
            {
                _prevPosition = _content.anchoredPosition;
                var position = _prevPosition;
                for (var axis = 0; axis < 2; axis++)
                {
                    if (_inertia)
                    {
                        _velocity[axis] *= Mathf.Pow(_decelerationRate, deltaTime);
                        if (Mathf.Abs(_velocity[axis]) < 1)
                        {
                            _velocity[axis] = 0;
                        }

                        position[axis] += _velocity[axis] * deltaTime;
                    }
                    else
                    {
                        _velocity[axis] = 0;
                    }
                }

                offset = position - _content.anchoredPosition; //CalculateOffset(position - _content.anchoredPosition);
                position += offset;
                _content.anchoredPosition = position;
                UpdateContentBounds();
            }

            if (_isDrag && _inertia)
            {
                Vector3 newVelocity = (_content.anchoredPosition - _prevPosition) / deltaTime;
                _velocity = Vector3.Lerp(_velocity, newVelocity, deltaTime * 10);
            }
        }

        private Vector2 CalculateOffset(Vector2 delta)
        {
            return InternalCalculateOffset(ref _viewBounds, ref _contentBounds, ref delta);
        }

        internal static Vector2 InternalCalculateOffset(ref Bounds viewBounds, ref Bounds contentBounds, ref Vector2 delta)
        {
            var offset = Vector2.zero;
            Vector2 min = contentBounds.min;
            Vector2 max = contentBounds.max;

            // min/max offset extracted to check if approximately 0 and avoid recalculating layout every frame (case 1010178)

            min.x += delta.x;
            min.y += delta.y;

            max.x += delta.x;
            max.y += delta.y;

            var maxOffset = viewBounds.max.x - max.x;
            var minOffset = viewBounds.min.x - min.x;

            if (minOffset < -0.001f)
            {
                offset.x = minOffset;
            }
            else if (maxOffset > 0.001f)
            {
                offset.x = maxOffset;
            }

            maxOffset = viewBounds.max.y - max.y;
            minOffset = viewBounds.min.y - min.y;

            if (maxOffset > 0.001f)
            {
                offset.y = maxOffset;
            }
            else if (minOffset < -0.001f)
            {
                offset.y = minOffset;
            }

            return offset;
        }
        */
    }
}
