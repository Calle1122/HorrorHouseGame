﻿namespace GameConstants
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
        // Human
        public const string JumpTriggerParam = "PlayJump";
        public const string WalkParam = "IsWalking";
        public const string PushingParam = "IsPushing";
        
        // Ghost
        public const string JumpParam = "IsInAir";
        public const string PossessingParam = "IsPossessing";

    }
}