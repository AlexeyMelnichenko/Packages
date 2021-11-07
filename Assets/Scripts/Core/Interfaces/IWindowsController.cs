using Core.UI;

namespace Core.Interfaces
{
    public interface IWindowsController
    {
        TWindow Open<TWindow, TIntent>(TIntent intent) where TWindow : WindowWithIntent<TIntent>
            where TIntent : EmptyIntent;

        TWindow Open<TWindow>() where TWindow : WindowBase;
        
        void OnCloseWindow(WindowBase window);
    }
}