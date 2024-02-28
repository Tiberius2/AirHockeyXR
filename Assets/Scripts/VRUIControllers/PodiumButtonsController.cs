using System.Collections;
using TT.Globals;
using TT.Managers;
using TT.Structs;
using TT.Templates;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TT.HelperClasses;


namespace TT.VRUIContrrolers
{
    public class PodiumButtonsController : MonoBehaviour
    {
        [SerializeField] private XRSimpleInteractable restartButton;
        [SerializeField] private XRSimpleInteractable openMenuButton;

        private PopupStruct popupStruct;

        private void OnEnable()
        {
            restartButton.selectEntered.AddListener(OnRestartButtonPressed);
            openMenuButton.selectEntered.AddListener(OnMenuButtonPressed);
        }

        private void OnDisable()
        {
            restartButton.selectEntered.RemoveAllListeners();
            openMenuButton.selectEntered.RemoveAllListeners();
        }

        private void OnRestartButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            popupStruct = new PopupStruct(GameConstants.PopupMessage, GameConstants.YesButtonText, GameConstants.NoButtonText, OnRestartYesButtonClicked, null);
            ScreenManager.Instance.OpenPopup(typeof(PopupTemplate), popupStruct);
        }

        private void OnRestartYesButtonClicked()
        {
            StartCoroutine(ResetGameWithDelay());
        }

        private void OnMenuButtonPressed(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(VRMainMenuScreen), true);
        }

        private IEnumerator ResetGameWithDelay()
        {
            ScoreManager.Instance.ResetScore();
            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.ResetObjectPositions();
            VRPlaceTableManager.Instance.ResetTimer();
            yield return new WaitForSecondsRealtime(GameConstants.ResetDuration);
            GameManager.Instance.CanPlayersMove = true;
        }
    }
}