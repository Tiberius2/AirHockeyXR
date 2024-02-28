using TMPro;
using TT.Enums;

namespace TT.HelperClasses
{
    public static class ARUIScoreHelper
    {
        public static void ScoreManagerOnScoreChangeUI(TMP_Text playerUIText, TMP_Text aiUIText, Score score, int scoreValue)
        {
            if (score == Score.PlayerScore)
            {
                AdjustUI(playerUIText, scoreValue);
            }
            else
            {
                AdjustUI(aiUIText, scoreValue);
            }
        }

        private static void AdjustUI(TMP_Text tmpText, int scoreValue)
        {
            tmpText.text = scoreValue.ToString();
        }
    }
}