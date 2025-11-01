using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project._Scripts.Managers.Global
{
    public class LevelManager : SingletonPersistence<LevelManager>
    {
        [SerializeField] private string _mainSceneName;
        [SerializeField] private string _loadingSceneName;
        [SerializeField] private string _safeZoneSceneName;
        
        public float LoadSceneProgress { get; private set; }

        public void LoadMainMenu()
        {
            LoadScene(_mainSceneName, false);
        }

        public void LoadSaveZone()
        {
            LoadScene(_safeZoneSceneName);
        }

        public void LoadBattle(string sceneName)
        {
            LoadScene(sceneName);
        }
        private async void LoadScene(string sceneName, bool uiManagerEnabled = true)
        {
            // Init Loader scene
            LoadSceneProgress = 0;
            UIManager.Instance?.DisableUIManager();
            await SceneManager.LoadSceneAsync(_loadingSceneName);
            
            // Start to load new scene
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            // Wait until the scene is almost loaded (progress goes up to 0.9f)
            do
            {
                await Task.Delay(100);
                LoadSceneProgress = scene.progress;
            } while (scene.progress < 0.9f);
            await Task.Delay(2000); // To show loading screen with delay
            
            // Optionally show 100% just before activation
            LoadSceneProgress = 1f;

            // Activate the loaded scene
            scene.allowSceneActivation = true;

            // Wait for the scene to finish activation
            while (!scene.isDone)
            {
                await Task.Delay(50);
            }
            
            // Activate new scene
            scene.allowSceneActivation = true;
            if (uiManagerEnabled)
                UIManager.Instance?.EnableUIManager();
        }
    }   
}
