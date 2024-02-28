using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.Managers;
using TT.HelperClasses;

namespace TT.UIControllers
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Camera uiCamera;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            uiCamera.gameObject.SetActive(true);

            startGameButton.onClick.AddListener(OnStartGameButtonPressed);
            settingsButton.onClick.AddListener(OnSettingsButtonPressed);
            quitButton.onClick.AddListener(OnQuitButtonPressed);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            uiCamera.gameObject.SetActive(false);

            startGameButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        private void OnStartGameButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(DifficultyMenuScreen), true);
        }

        private void OnSettingsButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(SettingsScreen), true);
        }

        private void OnQuitButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            Application.Quit();
        }
    }
}
