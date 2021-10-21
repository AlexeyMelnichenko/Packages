using Core.UI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationController : IInitializable
    {
        private readonly WindowsController _windowsController;
        
        public SDK.SDK SDK { get; } = new SDK.SDK();

        public ApplicationController(WindowsController windowsController)
        {
            _windowsController = windowsController;
        }
        
        public void Initialize()
        {
            Application.targetFrameRate = 60;

            SDK.Initialize();
            
            
        }
    }
}