using Project._Scripts.Managers.Global;
using UnityEngine;

namespace Project._Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public void ContinueButton()
        {
            UIManager.Instance.PauseMenuSwitcher();
        }

        public void QuitButton()
        {
            LevelManager.Instance.LoadMainMenu();
        }
    }
}
