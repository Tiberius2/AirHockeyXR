using UnityEngine;
using MixedReality.Toolkit.UX;
using UnityEngine.XR.Interaction.Toolkit;
using TT.BaseClasses;
using TT.HelperClasses;
using TT.Enums;
using TT.Managers;
using TT.Globals;
using TT.Structs;
using TT.Templates;

namespace TT.UIControllers
{
    public class HololensDifficultyScreen : BaseScreen
    {

        [SerializeField] private PressableButton easyDifficultyButton;
        [SerializeField] private PressableButton mediumDifficultyButton;
        [SerializeField] private PressableButton hardDifficultyButton;
        [SerializeField] private PressableButton backToMenuButton;

        private PopupStruct popupStruct;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            ScreenManager.Instance.ChangeSlateTitle(GameConstants.DifficultyMenuTitle);

            easyDifficultyButton.selectEntered.AddListener(OnEasyDifficultyButtonPressed);
            mediumDifficultyButton.selectEntered.AddListener(OnMediumDifficultyButtonPressed);
            hardDifficultyButton.selectEntered.AddListener(OnHardDifficultyButtonPressed);
            backToMenuButton.selectEntered.AddListener(OnBackToMenuButtonPressed);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();

            easyDifficultyButton.selectEntered.RemoveAllListeners();
            mediumDifficultyButton.selectEntered.RemoveAllListeners();
            hardDifficultyButton.selectEntered.RemoveAllListeners();
            backToMenuButton.selectEntered.RemoveAllListeners();
        }

        private void OnBackToMenuButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.ClosePopup(typeof(HoloPopupTemplate));
            ScreenManager.Instance.OpenScreen(typeof(HololensMainMenuScreen), true);
        }

        private void OnEasyDifficultyButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Easy);

        }

        private void OnMediumDifficultyButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Medium);
        }

        private void OnHardDifficultyButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Hard);
        }

        private void SetDifficulty(Difficulties selectedDifficulty)
        {
            popupStruct = new PopupStruct(GameConstants.SelectTableMessage, GameConstants.LargeTableText, GameConstants.SmallTableText, OnLargeSizeButtonPressed, OnSmallSizeButtonPressed);
            GameValues.DifficultySetting = selectedDifficulty;
            ScreenManager.Instance.OpenPopup(typeof(HoloPopupTemplate), popupStruct);

        }

        private void OnSmallSizeButtonPressed()
        {
            EyeTrackerManager.Instance.SetTableOption(false);
            StartGameSession();
        }

        private void OnLargeSizeButtonPressed()
        {
            EyeTrackerManager.Instance.SetTableOption(true);
            StartGameSession();
        }

        private void StartGameSession()
        {
            EyeTrackerManager.Instance.DestroyTable();
            ScreenManager.Instance.ClosePopup(typeof(HoloPopupTemplate));
            ScreenManager.Instance.CloseScreen();
            EyeTrackerManager.Instance.SetEyeTrackingState(true);
        }
    }
}