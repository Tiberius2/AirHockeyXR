namespace TT.Globals
{
    public static class GameConstants
    {
        #region AiBehaviourLargeTable
        public const float EasyMovementSpeed = 0.75f;
        public const float EasyOffsetRangeMin = -0.4f;
        public const float EasyOffsetRangeMax = 0.4f;
        public const float EasySpeedMultiplierMin = 0.2f;
        public const float EasySpeedMultiplierMax = 0.4f;

        public const float MediumMovementSpeed = 1f;
        public const float MediumOffsetRangeMin = -0.2f;
        public const float MediumOffsetRangeMax = 0.3f;
        public const float MediumSpeedMultiplierMin = 0.2f;
        public const float MediumSpeedMultiplierMax = 0.5f;

        public const float HardMovementSpeed = 1.5f;
        public const float HardOffsetRangeMin = -0.1f;
        public const float HardOffsetRangeMax = 0.2f;
        public const float HardSpeedMultiplierMin = 0.3f;
        public const float HardSpeedMultiplierMax = 0.6f;


        public const int OffsetMultiplier = 2;
        #endregion

        #region AIBehaviourSmallTable

        public const float EasyMovementSpeedSmallTable = 0.23f;
        public const float MediumMovementSpeedSmallTable = 0.3f;
        public const float HardMovementSpeedSmallTable = 0.55f;

        #endregion

        #region PuckBehaviour
        public const string HololensHandTag = "HololensHand";
        public const string AIGoalTag = "AIGoal";
        public const string PlayerGoalTag = "PlayerGoal";
        public const string PlayerStrikerTag = "PlayerStriker";
        public const string AiStrikerTag = "AiStriker";
        public const string PlayerPointWinSound = "PlayerPointWin";
        public const string AiPointWinSound = "AiPointWin";
        public const string EdgeImpactSound = "EdgeImpactSound";
        public const string StrikerImpactSound = "StrikerImpactSound";
        public const string TimerCollider = "TimerCollider";
        public const string CollisionLayer = "TableEdges";
        public const float ParticleOffset = 0.15f;
        public const float puckZPositionOnGoal = 0.3f;
        public const float PuckSpeedAndroidiOS = 3.5f;
        public const float PuckSpeedOculus = 4f;
        #endregion

        #region PlaceManager
        public const string PlacedObjectLayer = "PlacedObject";
        public const float Offset = 90f;
        public const float GroundOffset = 0.22f;
        #endregion

        #region ScoreManager
        public const int DefaultScore = 0;
        public const int MaxScore = 4;
        #endregion

        #region ScreenManager
        public const string ButtonClickSound = "ButtonClick";
        #endregion

        #region ARScreen
        public const string PlayerWinText = "You Won!\nPlay Again?";
        public const string AiWinText = "You Lost!\nPlay Again?";
        public const string WinSound = "WinSound";
        public const string LoseSound = "LoseSound";

        #endregion

        #region PopupInfo
        public const string PopupMessage = "Are you sure?";
        public const string SelectTableMessage = "Choose Table Size!";
        public const string YesButtonText = "Yes";
        public const string NoButtonText = "No";

        public const string LargeTableText = "LARGE";
        public const string SmallTableText = "SMALL";
        #endregion

        #region Coroutines
        public const float ResetDuration = 1f;
        #endregion

        #region Titles

        public const string MenuTitle = "MAIN MENU";
        public const string DifficultyMenuTitle = "CHOOSE DIFFICULTY";
        public const string SettingsMenuTitle = "SETTINGS";

        #endregion

        #region EyeTrackerManager
        public const float LerpSpeed = 5f;
        public const float AngleThreshold = 10f;
        public const float OffsetDistance = 0.02f;
        public const float ForwardDistance = 3f;
        #endregion
    }
}