using SingletonTemplate;
using System;
using TT.Enums;
using TT.Globals;

namespace TT.Managers
{
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public event Action<bool> OnGameFinished;
        public event Action<Score, int> OnScoreChange;
        public event Action<Score> OnScoreAnimation;

        private int aiScoreInt;
        private int playerScoreInt;

        public void IncrementScore(Score whichScore)
        {
            if (whichScore == Score.AIScore)
            {

                ++playerScoreInt;
                OnScoreChange?.Invoke(whichScore, playerScoreInt);
            }
            else
            {
                ++aiScoreInt;
                OnScoreChange?.Invoke(whichScore, aiScoreInt);
            }
            CheckForWinner();
        }

        public void CheckForWinner()
        {
            if (playerScoreInt >= GameConstants.MaxScore)
            {
                OnGameFinished?.Invoke(true);
            }
            else if (aiScoreInt >= GameConstants.MaxScore)
            {
                OnGameFinished?.Invoke(false);
            }
        }

        public void ResetScore()
        {
            playerScoreInt = GameConstants.DefaultScore;
            aiScoreInt = GameConstants.DefaultScore;
            OnScoreChange?.Invoke(Score.PlayerScore, playerScoreInt);
            OnScoreChange?.Invoke(Score.AIScore, aiScoreInt);
        }

        public void FlashScore(Score score)
        {
            OnScoreAnimation?.Invoke(score);
        }
    }
}
