using System.Collections.Generic;
using Core.UI;

namespace Core.Interfaces
{
    public interface IWindowsController
    {
        IList<WindowBase> OpenedWindows { get; }
        TWindow Open<TWindow, TIntent>(TIntent intent) where TWindow : WindowWithIntent<TIntent>
            where TIntent : EmptyIntent;

        TWindow Open<TWindow>() where TWindow : WindowBase;
    }
}