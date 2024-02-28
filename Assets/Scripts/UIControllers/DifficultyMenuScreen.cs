using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.HelperClasses;
using TT.Enums;
using TT.Managers;

namespace TT.UIControllers
{
    public class DifficultyMenuScreen : BaseScreen
    {
        [SerializeField] private Button easyDifficultyButton;
        [SerializeField] private Button mediumDifficultyButton;
        [SerializeField] private Button hardDifficultyButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private Camera uiCamera;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            uiCamera.gameObject.SetActive(true);

            easyDifficultyButton.onClick.AddListener(OnEasyDifficultyButtonPressed);
            mediumDifficultyButton.onClick.AddListener(OnMediumDifficultyButtonPressed);
            hardDifficultyButton.onClick.AddListener(OnHardDifficultyButtonPressed);
            backToMenuButton.onClick.AddListener(OnBackToMenuButtonPressed);
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            uiCamera.gameObject.SetActive(false);

            easyDifficultyButton.onClick.RemoveAllListeners();
            mediumDifficultyButton.onClick.RemoveAllListeners();
            hardDifficultyButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
        }
        private void OnBackToMenuButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(MainMenuScreen), true);
        }

        private void OnEasyDifficultyButtonPressed()
        {
            SwitchToARScreen(Difficulties.Easy);
        }

        private void OnMediumDifficultyButtonPressed()
        {
            SwitchToARScreen(Difficulties.Medium);
        }

        private void OnHardDifficultyButtonPressed()
        {
            SwitchToARScreen(Difficulties.Hard);
        }

        private void SwitchToARScreen(Difficulties selectedDifficulty)
        {
            ButtonSoundHelper.PlayButtonSound();
            GameValues.DifficultySetting = selectedDifficulty;
            ScreenManager.Instance.OpenScreen(typeof(ARScreen), true);
        }
    }
}
