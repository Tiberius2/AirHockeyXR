using TMPro;
using TT.Managers;
using UnityEngine;

namespace TT.Miscellaneous
{
    public class TimerController : MonoBehaviour
    {

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private float initialTime;
        [SerializeField] private Collider playerStrikerCollider;

        private float remainingTime;
        private bool isTimerRunning = false;
        private const int timerConstant = 60;

        private void Start()
        {
            remainingTime = initialTime;
        }
        private void Update()
        {
            if (isTimerRunning)
            {
                if (remainingTime > 0)
                {
                    playerStrikerCollider.enabled = false;
                    GameManager.Instance.CanPlayersMove = false;
                    remainingTime -= Time.deltaTime;
                }
                else if (remainingTime < 0)
                {
                    remainingTime = 0;
                    playerStrikerCollider.enabled = true;
                    GameManager.Instance.CanPlayersMove = true;

                    StopTimer();
                }
                UpdateTimerText();
            }
        }

        public void ResetTimer()
        {
            remainingTime = initialTime;
        }

        public void StartTimer()
        {
            isTimerRunning = true;
        }

        private void UpdateTimerText()
        {
            var minutes = Mathf.FloorToInt(remainingTime / timerConstant);
            var seconds = Mathf.FloorToInt(remainingTime % timerConstant);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }
    }
}
