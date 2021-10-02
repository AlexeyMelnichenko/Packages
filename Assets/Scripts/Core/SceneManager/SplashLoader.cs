using System.Threading.Tasks;
using UnityEngine;

namespace Core.SceneManager
{
    public class SplashLoader : MonoBehaviour
    {
        private async void Start()
        {
            var tasks = new[]
            {
                Task.Delay(5000),
                SDK.SDK.InitializeTask
            };
            
            await Task.WhenAny(tasks);
            
            //Show GDPR if needs

            await Task.WhenAll(SceneManagerEx.LoadSceneAsync(SceneNames.Game.ToString("G")),
                SceneManagerEx.LoadSceneAsync(SceneNames.UI.ToString("G")));
            
            await SceneManagerEx.UnloadSceneAsync(SceneNames.Splash.ToString("G"));
            
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(SceneNames.Game.ToString("G")));
        }
    }
}
