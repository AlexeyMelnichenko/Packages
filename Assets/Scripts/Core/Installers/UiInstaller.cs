using Core.Interfaces;
using Core.UI;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private WindowBase[] _registeredWindows;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WindowsController>().AsSingle().WithArguments(_uiRoot, _registeredWindows);
        }
    }
}