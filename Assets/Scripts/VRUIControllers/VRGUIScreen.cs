using System.Collections;
using TMPro;
using TT.Enums;
using TT.Globals;
using TT.Managers;
using TT.Miscellaneous;
using TT.Structs;
using TT.Templates;
using UnityEngine;
using TT.HelperClasses;

namespace TT.VRUIContrrolers
{
    public class VRGUIScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text aiScoreText;
        [SerializeField] private TMP_Text playerScoreText;
        [SerializeField] private TMP_Text aiScoreBarText;
        [SerializeField] private TMP_Text playerScoreBarText;
        [SerializeField] private TimerController timer;

        private PopupStruct popupStruct;

        private void OnEnable()
        {
            ScoreManager.Instance.OnScoreChange += ScoreManagerOnScoreChanged;
            ScoreManager.Instance.OnGameFinished += ScoreManagerOnGameFinished;
        }

        private void OnDisable()
        {
            ScoreManager.Instance.OnScoreChange -= ScoreManagerOnScoreChanged;
            ScoreManager.Instance.OnGameFinished -= ScoreManagerOnGameFinished;
        }
        private void ScoreManagerOnScoreChanged(Score score, int scoreValue)
        {
            if (score == Score.PlayerScore)
            {
                AdjustGUI(playerScoreText, scoreValue);
                AdjustGUI(playerScoreBarText, scoreValue);
            }
            else
            {
                AdjustGUI(aiScoreText, scoreValue);
                AdjustGUI(aiScoreBarText, scoreValue);
            }
        }
        private void AdjustGUI(TMP_Text tmpText, int scoreValue)
        {
            tmpText.text = scoreValue.ToString();
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
            OpenGameFinishedPopop(whoWonText, GameConstants.YesButtonText, GameConstants.NoButtonText);
        }

        private void OpenGameFinishedPopop(string message, string yesButtonText, string noButtonText)
        {
            popupStruct = new PopupStruct(message, yesButtonText, noButtonText, OnYesButtonPressed, OnNoButtonPressed);
            ScreenManager.Instance.OpenPopup(typeof(PopupTemplate), popupStruct);
        }

        private void OnYesButtonPressed()
        {
            HandlePopupChoice();
            timer.StartTimer();
            StartCoroutine(ResetGameWithDelay());
        }

        private void OnNoButtonPressed()
        {
            HandlePopupChoice();
            VRPlaceTableManager.Instance.DestroyTable();
            ScreenManager.Instance.OpenScreen(typeof(VRMainMenuScreen), true);
        }

        private void HandlePopupChoice()
        {
            ButtonSoundHelper.PlayButtonSound();
            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.CanPlayersMove = true;
            GameManager.Instance.ResetObjectPositions();
            ScoreManager.Instance.ResetScore();
        }

        private IEnumerator ResetGameWithDelay()
        {
            timer.ResetTimer();
            timer.StartTimer();
            yield return new WaitForSecondsRealtime(GameConstants.ResetDuration);
            GameManager.Instance.CanPlayersMove = true;
        }
    }
}