using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedToggle : MonoBehaviour, IPointerClickHandler
    {
        private Animator _toggleAnimator;
        [SerializeField] private bool _interactable = true;
        [SerializeField] private bool _isOn = true;
        private static readonly int Active = Animator.StringToHash("Active");
        private static readonly int Inactive = Animator.StringToHash("Inactive");
        private static readonly int Activating = Animator.StringToHash("Activating");
        private static readonly int Deactivating = Animator.StringToHash("Deactivating");

        public event Action<bool> ToggleValueChanged = b => { };
        public event Action<bool> InteractableChanged = b => { };

        public bool IsOn {
            get => _isOn;
            set => Set(value);
        }

        public bool Interactable {
            get => _interactable;
            set {
                if (_interactable == value) {
                    return;
                }

                _interactable = true;
                InteractableChanged(_interactable);
            }
        }

        private void Awake()
        {
            _toggleAnimator = GetComponent<Animator>();

            SetAnimationTrigger(_isOn ? Active : Inactive);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable) {
                return;
            }

            IsOn = !IsOn;
        }

        public void SetValueWithoutNotification(bool isOn)
        {
            Set(isOn, false);
        }

        private void Set(bool value, bool withCallback = true)
        {
            if (_isOn == value) {
                return;
            }

            _isOn = value;

            if (withCallback) {
                ToggleValueChanged(_isOn);
                SetAnimationTrigger(_isOn ? Activating : Deactivating);
            } else {
                SetAnimationTrigger(_isOn ? Active : Inactive);
            }
        }

        private void SetAnimationTrigger(int triggerId)
        {
            _toggleAnimator.SetTrigger(triggerId);
        }
    }
}
