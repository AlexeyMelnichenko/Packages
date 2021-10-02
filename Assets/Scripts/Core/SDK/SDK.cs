using System.Threading.Tasks;

namespace Core.SDK
{
    public class SDK
    {
        private static readonly TaskCompletionSource<bool> _initializeTadskCompletionSource = new TaskCompletionSource<bool>();
        public static Task<bool> InitializeTask => _initializeTadskCompletionSource.Task;
        public bool Initialized { get; private set; }

        public async void Initialize()
        {
            if (Initialized)
            {
                _initializeTadskCompletionSource.TrySetResult(Initialized);
            }

            await Task.Delay(2000);

            Initialized = true;
            _initializeTadskCompletionSource.SetResult(true);
        }
    }
}