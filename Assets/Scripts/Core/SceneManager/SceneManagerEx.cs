using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.SceneManager
{
    public static class SceneManagerEx
    {
        public static Task LoadSceneAsync(string sceneName)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var asyncLoader = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoader.completed += ao => taskCompletionSource.TrySetResult(true);
            return taskCompletionSource.Task;
        }
        
        public static Task UnloadSceneAsync(string sceneName)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var asyncUnloader = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            asyncUnloader.completed += ao => taskCompletionSource.TrySetResult(true);
            return taskCompletionSource.Task;
        }
    }
}