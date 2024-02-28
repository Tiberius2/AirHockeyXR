using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using TT.BaseClasses;
using TT.Managers;
using TT.Enums;
using TT.HelperClasses;
using TT.Globals;
using TT.Structs;
using TT.Templates;


namespace TT.UIControllers
{
    public class ARScreen : BaseScreen
    {
        [SerializeField] private Button placeObjectButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private TMP_Text aiUIText;
        [SerializeField] private TMP_Text playerUIText;
        [SerializeField] private GameObject aiScoreUI;
        [SerializeField] private GameObject playerScoreUI;
        [SerializeField] private GameObject inGameMenuScreen;

        private PopupStruct popupStruct;
        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            HandleUIElements(true);
            ARManager.Instance.EnableARSession();
            placeObjectButton.onClick.AddListener(OnPlaceObjectButtonPressed);
            backToMenuButton.onClick.AddListener(OnMenuButtonPressed);
            ScoreManager.Instance.OnScoreChange += ScoreManagerOnScoreChangeUI;
            ScoreManager.Instance.OnGameFinished += ScoreManagerOnGameFinished;
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            ARManager.Instance.DisableARSession();
            placeObjectButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
            ScoreManager.Instance.ResetScore();
            ScoreManager.Instance.OnScoreChange -= ScoreManagerOnScoreChangeUI;
            ScoreManager.Instance.OnGameFinished -= ScoreManagerOnGameFinished;
        }

        private void HandleUIElements(bool isActive)
        {
            aiScoreUI.SetActive(!isActive);
            playerScoreUI.SetActive(!isActive);
            placeObjectButton.gameObject.SetActive(isActive);
            aiUIText.gameObject.SetActive(!isActive);
            playerUIText.gameObject.SetActive(!isActive);
        }

        private void OnPlaceObjectButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            PlaceManager.Instance.PlaceObject();
            HandleUIElements(false);
        }

        private void ScoreManagerOnGameFinished(bool hasAiWon)
        {
            if (hasAiWon)
            {
                HandleGameOver(GameConstants.AiWinText, GameConstants.LoseSound);
            }
            else
            {
                HandleGameOver(GameConstants.PlayerWinText, GameConstants.WinSound);
            }
        }

        private void HandleGameOver(string whoWonText, string whoWonSound)
        {
            GameManager.Instance.IsGameOver = true;
            GameManager.Instance.CanPlayersMove = false;
            AudioManager.Instance.PlaySound(whoWonSound);
            OpenGameFinishedPopup(whoWonText, GameConstants.YesButtonText, GameConstants.NoButtonText);
        }

        private void OnMenuButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(InGameMenuScreen), false);
        }

        private void ScoreManagerOnScoreChangeUI(Score score, int scoreValue)
        {
            ARUIScoreHelper.ScoreManagerOnScoreChangeUI(playerUIText, aiUIText, score, scoreValue);
        }

        private void OpenGameFinishedPopup(string message, string yesButtonText, string noButtonText)
        {
            popupStruct = new PopupStruct(message, yesButtonText, noButtonText, OnYesButtonClicked, OnNoButtonClicked);
            ScreenManager.Instance.OpenPopup(typeof(PopupTemplate), popupStruct);
        }

        private void OnYesButtonClicked()
        {
            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.ResetObjectPositions();
            ScoreManager.Instance.ResetScore();
            StartCoroutine(ResetGameWithDelay());
        }

        private void OnNoButtonClicked()
        {
            ScreenManager.Instance.OpenScreen(typeof(MainMenuScreen), true);
        }
        private IEnumerator ResetGameWithDelay()
        {
            yield return new WaitForSecondsRealtime(GameConstants.ResetDuration);
            GameManager.Instance.CanPlayersMove = true;
        }
    }
}
