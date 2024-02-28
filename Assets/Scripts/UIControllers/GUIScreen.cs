using TMPro;
using UnityEngine;
using TT.Enums;
using TT.Managers;

namespace TT.UIControllers
{
    public class GUIScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text aiTextMeshPro;
        [SerializeField] private TMP_Text playerTextMeshPro;

        private void OnEnable()
        {
            ScoreManager.Instance.OnScoreChange += ScoreManagerOnScoreChanged;
            ScoreManager.Instance.OnScoreAnimation += ScoreManagerOnScoreAnimation;
        }
        private void ScoreManagerOnScoreAnimation(Score score)
        {
            // TO BE IMPLEMENTED
        }

        private void ScoreManagerOnScoreChanged(Score score, int scoreValue)
        {
            if (score == Score.PlayerScore)
            {
                AdjustGUI(playerTextMeshPro, scoreValue);
            }
            else
            {
                AdjustGUI(aiTextMeshPro, scoreValue);
            }
        }

        private void AdjustGUI(TMP_Text tmpText, int scoreValue)
        {
            tmpText.text = scoreValue.ToString();
        }
    }
}