using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public static class Settings
    {
        public class Player
        {
            internal static float PlayerMovementSpeed = 7f;
            internal static float VerticalSensitivity = 2f;
            internal static float HorizontalSensitivity = 1.5f;
            internal static float CameraSensitivity = 0.8f;
        }

        public class Game
        {
            internal static float CharacterSpeed = 3f;
            internal static float MinHandHeight = 0.1f;
            internal static float CharacterRecoverVelocity = 0.1f;
            internal static float DistanceRemainingToWayPoint = 0.01f;
            internal static float SwipeQualifier = 0.3f;
            public static float LiftSpeed = 0.7f;
        }

        public class DefaultSettings
        {
            internal static float PlayerMovementSpeed = 7f;
            internal static float VerticalSensitivity = 1.5f;
            internal static float HorizontalSensitivity = 1.5f;

            internal static float CharacterSpeed = 3f;
            internal static float MinHandHeight = 0.1f;
            internal static float CharacterRecoverVelocity = 0.1f;
            internal static float DistanceRemainingToWayPoint = 0.01f;
            internal static float SwipeQualifier = 0.3f;
        }

        public class Colors
        {
            internal static Color32 Hover = new Color32(216,216,216,255);
            internal static Color32 Selected = new Color32(100,247,48,255);
            internal static Color32 PressurePlateActive = new Color32(248, 47, 47, 255);
            internal static Color32 PressurePlateInactive = new Color32(56, 204, 56, 255);
        }
    }
}
