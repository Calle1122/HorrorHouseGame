namespace GameConstants
{
    public static class Tags
    {
        public const string PlayerTag = "Player";
        public static string GhostTag = "Ghost";
    }

    public static class Variables
    {
    }

    public static class Strings
    {
        public const string ControllerControlScheme = "Controller";
        public const string KeyboardControlScheme = "Keyboard";

        // Input Actions
        public const string Move = "Move";
        public const string Interact = "Interact";
        public const string Jump = "Jump";
        public const string Cancel = "Cancel";

        // Music Channels
        public const string MasterVol = "MasterVolume";
        public const string MusicVol = "MusicVolume";
        public const string SfxVol = "SfxVolume";
        public const string DialogueVol = "DialogueVolume";
        public const string AmbientVol = "AmbientVolume";
        
        // Animation Parameters
        public const string PlayOnceTrigger = "playOnce";
        
        
        // Human
        // Bool Parameters
        public const string WalkParam = "IsWalking";
        public const string PushingCouchParam = "PushingCouch";
        public const string PushingCabinetParam = "PushCabinPlay";
        public const string PlacePickUpFloor = "PlaceRitualItemPlay";
        
        // Trigger Parameters
        public const string LerpStepTrigger = "LerpStepPlay";
        public const string PushFinishedTrigger = "PushFinished";
        public const string GenericPickUpTrigger = "GenericPickupPlay";
        public const string PumpKickTriggerParameter = "DancePumpKickPlay";
        public const string JumpTriggerParameter = "PlayJump";
        
        // Ghost
        // Bool Parameters
        public const string JumpParam = "IsInAir";
        
        // Trigger Parameters
        public const string PossessTrigger = "PossessPlay";
        public const string UnPossessTrigger = "UnpossessPlay";
        public const string StompTrigger = "SmushSpiderPlay";
    }
}