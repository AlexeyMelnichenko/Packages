using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationController : IInitializable
    {
        public IWindowsController WindowsController { get; }
        
        public SDK.SDK SDK { get; } = new SDK.SDK();

        public ApplicationController(IWindowsController windowsController)
        {
            WindowsController = windowsController;
        }
        
        public void Initialize()
        {
            Application.targetFrameRate = 60;

            SDK.Initialize();
            
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }
    }
}