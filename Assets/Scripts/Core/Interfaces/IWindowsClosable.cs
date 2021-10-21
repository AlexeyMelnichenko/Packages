namespace Core.Interfaces
{
    public interface IWindowsClosable
    {
        void SetClosingObserver(IWindowsObserver windowsObserver);
    }
}