using System.Threading.Tasks;
using Core.Interfaces;
using UnityEngine;

namespace Core.UI
{
    public class WindowBase : MonoBehaviour, IWindowsClosable
    {
        [SerializeField] protected GameObject _content;
        [SerializeField] private bool _isNeedCache = true;
        private IWindowsObserver _windowsObserver;
        private bool _isOpened;
        private TaskCompletionSource<bool> _openTaskSource;
        protected bool _closeResult = false;
        
        public Task<bool> CloseTask => _openTaskSource?.Task ?? Task.FromResult(_closeResult);
        public bool IsNeedCache => _isNeedCache;

        public async void Open()
        {
            if (_isOpened)
            {
                return;
            }
            
            OnOpening();
            OpenInternal();
            await AnimateOpening();
            OnOpened();
        }

        private void OpenInternal()
        {
            _content.SetActive(true);
            _openTaskSource = new TaskCompletionSource<bool>();
            _isOpened = true;
        }

        protected virtual void OnOpening() { }

        protected virtual Task AnimateOpening()
        {
            return Task.CompletedTask;
        }
        
        protected virtual void OnOpened() { }

        public async void Close()
        {
            if(!_isOpened)
            {
                return;
            }
            
            OnClosing();
            await AnimateClosing();
            CloseInternal();
            OnClosed();
            
            _windowsObserver.OnCloseWindow(this);
        }

        private void CloseInternal()
        {
            _content.SetActive(false);
            _openTaskSource.SetResult(_closeResult);
            _isOpened = true;
        }
        
        protected virtual void OnClosing() { }

        protected virtual Task AnimateClosing()
        {
            return Task.CompletedTask;
        }
        
        protected virtual void OnClosed() { }

        public void Push()
        {
            transform.SetAsLastSibling();
        }

        void IWindowsClosable.SetClosingObserver(IWindowsObserver windowsObserver)
        {
            _windowsObserver = windowsObserver;
        }
    }
}