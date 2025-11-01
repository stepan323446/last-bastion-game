using Project._Scripts.Managers.Global;
using UnityEngine;

namespace Project._Scripts.UI.MainMenu
{
    public class PrimaryMenu : MonoBehaviour
    {
        public void LoadTestScene(string sceneName)
        {
            LevelManager.Instance.LoadBattle(sceneName);
        }
    }
   
}