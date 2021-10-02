using Zenject;

namespace Core.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationController>().AsSingle();
        }
    }
}