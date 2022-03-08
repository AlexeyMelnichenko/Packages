using Zenject;

namespace Core.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSdk();
            BindClasses();
        }

        private void BindSdk()
        {
            Container.BindInterfacesTo<SDK.SDK>().AsSingle().NonLazy();
            OnBindSdk();
        }

        protected virtual void OnBindSdk() { }

        protected virtual void BindClasses()
        {
            Container.BindInterfacesAndSelfTo<ApplicationController>().AsSingle();
        }
    }
}