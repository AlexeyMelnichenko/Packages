using Core.UI;

namespace Core.Interfaces
{
    public interface IWindowsObserver
    {
        void OnCloseWindow(WindowBase window);
    }
}