﻿using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using StardewValley.Tools;

namespace TehPers.FishingOverhaul.Extensions.Drawing
{
    public record MeleeWeaponDrawingProperties(int Type, bool IsScythe) : IDrawingProperties
    {
        private static readonly FieldInfo addedSwordScale =
            typeof(MeleeWeapon).GetField(
                nameof(MeleeWeaponDrawingProperties.addedSwordScale),
                BindingFlags.Static | BindingFlags.NonPublic
            )
            ?? throw new Exception($"Missing info for {nameof(MeleeWeaponDrawingProperties.addedSwordScale)}.");

        private static readonly FieldInfo addedDaggerScale =
            typeof(MeleeWeapon).GetField(
                nameof(MeleeWeaponDrawingProperties.addedDaggerScale),
                BindingFlags.Static | BindingFlags.NonPublic
            )
            ?? throw new Exception($"Missing info for {nameof(MeleeWeaponDrawingProperties.addedDaggerScale)}.");

        private static readonly FieldInfo addedClubScale =
            typeof(MeleeWeapon).GetField(
                nameof(MeleeWeaponDrawingProperties.addedClubScale),
                BindingFlags.Static | BindingFlags.NonPublic
            )
            ?? throw new Exception($"Missing info for {nameof(MeleeWeaponDrawingProperties.addedClubScale)}.");

        public Vector2 SourceSize => new(16, 16);

        public Vector2 Offset(float scaleSize) => this.Type switch
        {
            1 => new(38f, 25f),
            _ => new(32f, 32f),
        };

        public Vector2 Origin(float scaleSize) => new(8f, 8f);

        public float RealScale(float scaleSize)
        {
            var addedScale = (this.Type, this.IsScythe) switch
            {
                (_, true) => 0f,
                (0, _) => (float)MeleeWeaponDrawingProperties.addedSwordScale.GetValue(null)!,
                (3, _) => (float)MeleeWeaponDrawingProperties.addedSwordScale.GetValue(null)!,
                (1, _) => (float)MeleeWeaponDrawingProperties.addedDaggerScale.GetValue(null)!,
                (2, _) => (float)MeleeWeaponDrawingProperties.addedClubScale.GetValue(null)!,
                _ => 0f
            };
            return 4f * (scaleSize + addedScale);
        }
    }
}