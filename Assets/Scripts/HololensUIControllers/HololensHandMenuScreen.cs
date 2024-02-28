using MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TT.BaseClasses;
using TT.Globals;
using TT.Managers;
using TT.Structs;
using TT.Templates;
using TT.HelperClasses;


namespace TT.UIControllers
{
    public class HololensHandMenuScreen : BaseScreen
    {
        [SerializeField] private PressableButton resumeButton;
        [SerializeField] private PressableButton restartButton;
        [SerializeField] private PressableButton settingsButton;
        [SerializeField] private PressableButton mainMenuButton;

        private PopupStruct popupStruct;
        private void OnEnable()
        {
            resumeButton.selectEntered.AddListener(OnResumeButtonPressed);
            restartButton.selectEntered.AddListener(OnRestartButtonPressed);
            settingsButton.selectEntered.AddListener(OnSettingsButtonPressed);
            mainMenuButton.selectEntered.AddListener(OnMainMenuButtonPressed);

        }

        private void OnDisable()
        {
            resumeButton.selectEntered.RemoveAllListeners();
            restartButton.selectEntered.RemoveAllListeners();
            settingsButton.selectEntered.RemoveAllListeners();
            mainMenuButton.selectEntered.RemoveAllListeners();
        }
        private void OnResumeButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            gameObject.SetActive(false);
        }

        private void OnRestartButtonPressed(SelectEnterEventArgs arg0)
        {
            popupStruct = new PopupStruct(GameConstants.PopupMessage, GameConstants.YesButtonText, GameConstants.NoButtonText, OnRestartYesButtonClicked, null);
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenPopup(typeof(HoloPopupTemplate), popupStruct);
        }

        private void OnSettingsButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.CloseScreen();
            ScreenManager.Instance.OpenScreen(typeof(HololensSettingsScreen), false, true);
        }

        private void OnMainMenuButtonPressed(SelectEnterEventArgs arg0)
        {
            popupStruct = new PopupStruct(GameConstants.PopupMessage, GameConstants.YesButtonText, GameConstants.NoButtonText, OnBackToMenuYesButtonClicked, null);
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenPopup(typeof(HoloPopupTemplate), popupStruct);
        }

        private void OnBackToMenuYesButtonClicked()
        {
            EyeTrackerManager.Instance.SetEyeTrackingState(false);
            EyeTrackerManager.Instance.DestroyTable();
            gameObject.SetActive(false);
            ScreenManager.Instance.OpenScreen(typeof(HololensMainMenuScreen), true);

        }

        private void OnRestartYesButtonClicked()
        {
            HandleGameRestart();
        }

        private void HandleGameRestart()
        {
            ScreenManager.Instance.CloseScreen();
            EyeTrackerManager.Instance.DestroyTable();
            EyeTrackerManager.Instance.SetEyeTrackingState(true);
            ScoreManager.Instance.ResetScore();
            GameManager.Instance.ResetObjectPositions();
            gameObject.SetActive(false);
        }
    }
}
