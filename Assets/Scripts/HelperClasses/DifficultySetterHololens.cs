using TT.Behaviours;
using TT.Enums;
using TT.Globals;
using TT.Managers;
using UnityEngine;

namespace TT.HelperClasses
{
    public static class DifficultySetterHololens
    {
        public static void SetDifficultyValues(AIBehaviour aiBehaviour, Difficulties difficulty)
        {
            float maxMovementSpeed;

            if (IsWindowsPlatform())
            {
                if (EyeTrackerManager.Instance.CheckTableOption())
                {
                    maxMovementSpeed = GetMovementSpeedForDifficulty(difficulty, false);
                    SetCommonValues(aiBehaviour, difficulty);
                }
                else
                {
                    maxMovementSpeed = GetMovementSpeedForDifficulty(difficulty, true);
                    SetCommonValues(aiBehaviour, difficulty);
                    AdjustForTableOption(aiBehaviour);
                }
            }
            else
            {
                maxMovementSpeed = GetMovementSpeedForDifficulty(difficulty, false);
                SetCommonValues(aiBehaviour, difficulty);
            }
            aiBehaviour.MaxMovementSpeed = maxMovementSpeed;
        }

        private static bool IsWindowsPlatform()
        {
            return Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        }

        private static void SetCommonValues(AIBehaviour aiBehaviour, Difficulties difficulty)
        {
            aiBehaviour.MinOffsetRange = GetDifficultyValue(difficulty, GameConstants.EasyOffsetRangeMin, GameConstants.MediumOffsetRangeMin, GameConstants.HardOffsetRangeMin);
            aiBehaviour.MaxOffsetRange = GetDifficultyValue(difficulty, GameConstants.EasyOffsetRangeMax, GameConstants.MediumOffsetRangeMax, GameConstants.HardOffsetRangeMax);
            aiBehaviour.MinMovementSpeedMultipier = GetDifficultyValue(difficulty, GameConstants.EasySpeedMultiplierMin, GameConstants.MediumSpeedMultiplierMin, GameConstants.HardSpeedMultiplierMin);
            aiBehaviour.MaxMovementSpeedMultipier = GetDifficultyValue(difficulty, GameConstants.EasySpeedMultiplierMax, GameConstants.MediumSpeedMultiplierMax, GameConstants.HardSpeedMultiplierMax);
        }

        private static float GetMovementSpeedForDifficulty(Difficulties difficulty, bool isSmallTable)
        {
            return difficulty switch
            {
                Difficulties.Easy => isSmallTable ? GameConstants.EasyMovementSpeedSmallTable : GameConstants.EasyMovementSpeed,
                Difficulties.Medium => isSmallTable ? GameConstants.MediumMovementSpeedSmallTable : GameConstants.MediumMovementSpeed,
                Difficulties.Hard => isSmallTable ? GameConstants.HardMovementSpeedSmallTable : GameConstants.HardMovementSpeed,
                _ => 0f,
            };
        }

        private static void AdjustForTableOption(AIBehaviour aiBehaviour)
        {
            aiBehaviour.MinOffsetRange *= GameConstants.OffsetMultiplier;
            aiBehaviour.MaxOffsetRange *= GameConstants.OffsetMultiplier;
            aiBehaviour.MinMovementSpeedMultipier /= GameConstants.OffsetMultiplier;
            aiBehaviour.MaxMovementSpeedMultipier /= GameConstants.OffsetMultiplier;
        }

        private static float GetDifficultyValue(Difficulties difficulty, float easyValue, float mediumValue, float hardValue)
        {
            return difficulty switch
            {
                Difficulties.Easy => easyValue,
                Difficulties.Medium => mediumValue,
                Difficulties.Hard => hardValue,
                _ => 0f,
            };
        }
    }
}