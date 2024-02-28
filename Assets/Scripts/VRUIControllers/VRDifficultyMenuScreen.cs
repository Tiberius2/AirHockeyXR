using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.Managers;
using TT.Enums;
using TT.HelperClasses;
using TT.Templates;

namespace TT.VRUIContrrolers
{
    public class VRDifficultyMenuScreen : BaseScreen
    {
        [SerializeField] private Button easyDifficultyButton;
        [SerializeField] private Button mediumDifficultyButton;
        [SerializeField] private Button hardDifficultyButton;
        [SerializeField] private Button backToMenuButton;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            easyDifficultyButton.onClick.AddListener(OnEasyDifficultySelected);
            mediumDifficultyButton.onClick.AddListener(OnMediumDifficultySelected);
            hardDifficultyButton.onClick.AddListener(OnHardDifficultySelected);
            backToMenuButton.onClick.AddListener(OnBackToMenuButtonPressed);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            easyDifficultyButton.onClick.RemoveAllListeners();
            mediumDifficultyButton.onClick.RemoveAllListeners();
            hardDifficultyButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
        }
        private void OnEasyDifficultySelected()
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Easy);
        }

        private void OnMediumDifficultySelected()
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Medium);
        }

        private void OnHardDifficultySelected()
        {
            ButtonSoundHelper.PlayButtonSound();
            SetDifficulty(Difficulties.Hard);
        }

        private void OnBackToMenuButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(VRMainMenuScreen), true);
        }

        private void SetDifficulty(Difficulties selectedDifficulty)
        {
            GameValues.DifficultySetting = selectedDifficulty;
            VRPlaceTableManager.Instance.DestroyTable();
            VRPlaceTableManager.Instance.InstantiateTable();
            ScreenManager.Instance.ClosePopup(typeof(PopupTemplate));
            ScreenManager.Instance.CloseScreen();
        }
    }
}