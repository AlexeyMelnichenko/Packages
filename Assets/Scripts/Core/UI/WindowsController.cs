using System;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core.UI
{
    public class WindowsController : IWindowsController, IWindowsContainer
    {
        private readonly Transform _uiRoot;
        private readonly Dictionary<Type, WindowBase> _registeredWindows;
        private readonly List<WindowBase> _openedWindows = new List<WindowBase>();
        private readonly Dictionary<Type, WindowBase> _cachedWindows = new Dictionary<Type, WindowBase>();

        public IList<WindowBase> OpenedWindows => _openedWindows;

        public WindowsController(Transform uiRoot, WindowBase[] registeredWindows)
        {
            _uiRoot = uiRoot;

            _registeredWindows = new Dictionary<Type, WindowBase>(registeredWindows.Length);
            foreach (var window in registeredWindows)
            {
                RegisterWindow(window);
            }
        }

        public void RegisterWindow(WindowBase window)
        {
            var windowType = window.GetType();
            
            if (!_registeredWindows.ContainsKey(windowType))
            {
                _registeredWindows[windowType] = window;
            }
        }

        public TWindow Open<TWindow, TIntent>(TIntent intent) where TWindow : WindowWithIntent<TIntent> where TIntent : EmptyIntent
        {
            var window = GetWindow<TWindow>();
            ((IIntentSetter<TIntent>)window).SetIntent(intent);
            OpenWindow(window);
            return (TWindow)window;
        }

        public TWindow Open<TWindow>() where TWindow : WindowBase
        {
            var window = GetWindow<TWindow>();
            OpenWindow(window);
            return (TWindow)window;
        }

        private void OpenWindow(WindowBase window)
        {
            if (_openedWindows.Contains(window))
            {
                window.Push();
            }
            else
            {
                window.Open();
                _openedWindows.Add(window);
            }
        }

        private WindowBase GetWindow<TWindow>() where TWindow : WindowBase
        {
            var windowType = typeof(TWindow);
            if (_registeredWindows.ContainsKey(windowType))
            {
                if (_cachedWindows.ContainsKey(windowType))
                {
                    return _cachedWindows[windowType];
                }

                var windowPrefab = _registeredWindows[windowType];
                var window = UnityEngine.Object.Instantiate(windowPrefab, _uiRoot);
                ((IWindowsControllerContainer)window).SetWindowsController(this);
                _cachedWindows[windowType] = window;
                return window;
            }

            throw new Exception($"Window {windowType.Name} is not registered!");
        }

        public void OnCloseWindow(WindowBase window)
        {
            if (_openedWindows.Contains(window))
            {
                _openedWindows.Remove(window);
            }

            var type = window.GetType();

            if (window.IsNeedCache || !_cachedWindows.ContainsKey(type))
            {
                return;
            }
            
            _cachedWindows.Remove(type);
            UnityEngine.Object.Destroy(window.gameObject, 1f);
        }
    }
}