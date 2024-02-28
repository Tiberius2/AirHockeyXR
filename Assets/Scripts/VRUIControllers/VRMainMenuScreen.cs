using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.Managers;
using TT.HelperClasses;


namespace TT.VRUIContrrolers
{
    public class VRMainMenuScreen : BaseScreen
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitGameButton;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            startGameButton.onClick.AddListener(OnStartGameButtonPressed);
            settingsButton.onClick.AddListener(OnSettingsButtonPressed);
            quitGameButton.onClick.AddListener(OnQuitGameButtonPresssd);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            startGameButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            quitGameButton.onClick.RemoveAllListeners();
        }
        private void OnStartGameButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(VRDifficultyMenuScreen), true);
        }

        private void OnSettingsButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(VRSettingsScreen), true);
        }

        private void OnQuitGameButtonPresssd()
        {
            ButtonSoundHelper.PlayButtonSound();
            Application.Quit();
        }
    }
}
