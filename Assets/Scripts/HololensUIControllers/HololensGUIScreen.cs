using TMPro;
using UnityEngine;
using TT.Enums;
using TT.Managers;
using TT.Globals;
using TT.Structs;
using TT.Templates;
using TT.HelperClasses;
using System.Collections;

namespace TT.UIControllers
{
    public class HololensGUIScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text aiTextMeshPro;
        [SerializeField] private TMP_Text playerTextMeshPro;
        [SerializeField] private TMP_Text aiScoreBarText;
        [SerializeField] private TMP_Text playerScoreBarText;

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
                AdjustGUI(playerTextMeshPro, scoreValue);
                AdjustGUI(playerScoreBarText, scoreValue);
            }
            else
            {
                AdjustGUI(aiTextMeshPro, scoreValue);
                AdjustGUI(aiScoreBarText, scoreValue);

            }
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

        private void OpenGameFinishedPopup(string message, string yesButtonText, string noButtonText)
        {
            popupStruct = new PopupStruct(message, yesButtonText, noButtonText, OnYesPopupButtonClicked, OnNoButtonPopupClicked);
            ScreenManager.Instance.OpenPopup(typeof(HoloPopupTemplate), popupStruct);
        }

        private void OnYesPopupButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.CanPlayersMove = true;
            GameManager.Instance.ResetObjectPositions();
            ScoreManager.Instance.ResetScore();
            StartCoroutine(ResetGameWithDelay());
        }

        private void OnNoButtonPopupClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            GameManager.Instance.CanPlayersMove = true;
            EyeTrackerManager.Instance.SetEyeTrackingState(false);
            EyeTrackerManager.Instance.DestroyTable();
            ScoreManager.Instance.ResetScore();
            ScreenManager.Instance.OpenScreen(typeof(HololensMainMenuScreen), true);
        }

        private void AdjustGUI(TMP_Text tmpText, int scoreValue)
        {
            tmpText.text = scoreValue.ToString();
        }

        private IEnumerator ResetGameWithDelay()
        {
            yield return new WaitForSecondsRealtime(GameConstants.ResetDuration);
            GameManager.Instance.CanPlayersMove = true;
        }
    }
}