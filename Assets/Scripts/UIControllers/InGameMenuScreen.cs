using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.Managers;
using TT.Globals;
using TT.Structs;
using TT.Templates;
using TT.HelperClasses;

namespace TT.UIControllers
{
    public class InGameMenuScreen : BaseScreen
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backToMainMenuButton;

        private PopupStruct popupStruct;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            backToMainMenuButton.onClick.AddListener(OnBackToMenuClicked);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            resumeButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            backToMainMenuButton.onClick.RemoveAllListeners();
        }

        private void OnBackToMenuClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            popupStruct = new PopupStruct(GameConstants.PopupMessage, GameConstants.YesButtonText, GameConstants.NoButtonText, OnBackToMenuYesButtonClicked, null);
            ScreenManager.Instance.OpenPopup(typeof(PopupTemplate), popupStruct);
        }

        private void OnSettingsButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(SettingsScreen), false, true);
        }

        private void OnRestartButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            popupStruct = new PopupStruct(GameConstants.PopupMessage, GameConstants.YesButtonText, GameConstants.NoButtonText, OnRestartYesButtonClicked, null);
            ScreenManager.Instance.OpenPopup(typeof(PopupTemplate),popupStruct);
        }

        private void OnResumeButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.CloseScreen();
        }

        private void OnBackToMenuYesButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(MainMenuScreen), true);
        }

        private void OnRestartYesButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(ARScreen), true);
        }
    }
}