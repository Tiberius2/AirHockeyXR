using UnityEngine;
using MixedReality.Toolkit.UX;
using UnityEngine.XR.Interaction.Toolkit;
using TT.BaseClasses;
using TT.Managers;
using TT.Globals;
using TT.HelperClasses;

namespace TT.UIControllers
{
    public class HololensMainMenuScreen : BaseScreen
    {

        [SerializeField] private PressableButton startGameButton;
        [SerializeField] private PressableButton settingsButton;
        [SerializeField] private PressableButton quitGameButton;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            ScreenManager.Instance.ChangeSlateTitle(GameConstants.MenuTitle);

            startGameButton.selectEntered.AddListener(OnStartGameButtonPressed);
            settingsButton.selectEntered.AddListener(OnSettingsButtonPressed);
            quitGameButton.selectEntered.AddListener(OnQuitButtonPressed);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            startGameButton.selectEntered.RemoveAllListeners();
            settingsButton.selectEntered.RemoveAllListeners();
            quitGameButton.selectEntered.RemoveAllListeners();
        }

        private void OnStartGameButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(HololensDifficultyScreen), true);
        }

        private void OnSettingsButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(HololensSettingsScreen), true);
        }

        private void OnQuitButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            Application.Quit();
        }
    }
}

