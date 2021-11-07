using System.Threading.Tasks;

namespace Core.SDK
{
    public class SDK
    {
        private static readonly TaskCompletionSource<bool> _initializeTaskCompletionSource = new TaskCompletionSource<bool>();
        public static Task<bool> InitializeTask => _initializeTaskCompletionSource.Task;
        public bool Initialized { get; private set; }

        public async void Initialize()
        {
            if (Initialized)
            {
                _initializeTaskCompletionSource.TrySetResult(Initialized);
            }

            //simulation of download
            await Task.Delay(2000);

            Initialized = true;
            _initializeTaskCompletionSource.SetResult(true);
        }
    }
}