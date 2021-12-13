﻿using System.ComponentModel;
using StardewModdingAPI;
using TehPers.Core.Api.Json;
using TehPers.FishingOverhaul.Integrations.GenericModConfigMenu;

namespace TehPers.FishingOverhaul.Config
{
    /// <summary>
    /// Configuration for fish.
    /// </summary>
    /// <inheritdoc cref="IModConfig"/>
    [JsonDescribe]
    public sealed class FishConfig : IModConfig
    {
        /// <summary>
        /// Whether to show the fish being caught in the fishing minigame.
        /// </summary>
        [DefaultValue(false)]
        public bool ShowFishInMinigame { get; set; }

        /// <summary>
        /// Affects how fast you catch fish.
        /// </summary>
        [DefaultValue(1f)]
        public float CatchSpeed { get; set; } = 1f;

        /// <summary>
        /// Affects how fast the catch bar drains when the bobber isn't on the fish.
        /// </summary>
        [DefaultValue(1f)]
        public float DrainSpeed { get; set; } = 1f;

        /// <summary>
        /// Required streak for an increase in quality. For example, 3 means that every 3
        /// consecutive perfect catches increases your catch quality by 1.
        /// </summary>
        [DefaultValue(3)]
        public int StreakForIncreasedQuality { get; set; } = 3;

        /// <summary>
        /// The max quality fish that can be caught. 0 = normal, 1 = silver, 2 = gold, 3 = iridium,
        /// 4+ = beyond iridium.
        /// </summary>
        [DefaultValue(3)]
        public int MaxFishQuality { get; set; } = 3;

        /// <summary>
        /// Determines the max quality fish a non-perfect catch can get, or null for no
        /// restrictions.
        /// </summary>
        [DefaultValue(null)]
        public int? MaxNormalFishQuality { get; set; }

        /// <summary>
        /// The chance that you'll find a fish instead of trash.
        /// </summary>
        public FishingChances FishChances { get; init; } = new()
        {
            BaseChance = 0.5,
            StreakFactor = 0.005,
            FishingLevelFactor = 0.025,
            DailyLuckFactor = 1,
            LuckLevelFactor = 0.01,
            MinChance = 0.1,
            MaxChance = 0.9,
        };

        void IModConfig.Reset()
        {
            this.ShowFishInMinigame = false;
            this.CatchSpeed = 1f;
            this.DrainSpeed = 1f;
            this.StreakForIncreasedQuality = 3;
            this.MaxNormalFishQuality = null;
            this.MaxFishQuality = 3;

            // Fish chances
            this.FishChances.BaseChance = 0.5;
            this.FishChances.StreakFactor = 0.005;
            this.FishChances.FishingLevelFactor = 0.025;
            this.FishChances.DailyLuckFactor = 1;
            this.FishChances.LuckLevelFactor = 0.01;
            this.FishChances.MinChance = 0.1;
            this.FishChances.MaxChance = 0.9;
        }

        void IModConfig.RegisterOptions(
            IGenericModConfigMenuApi configApi,
            IManifest manifest,
            ITranslationHelper translations
        )
        {
            Translation Name(string key) => translations.Get($"text.config.fish.{key}.name");
            Translation Desc(string key) => translations.Get($"text.config.fish.{key}.desc");

            configApi.AddBoolOption(
                manifest,
                () => this.ShowFishInMinigame,
                val => this.ShowFishInMinigame = val,
                () => Name("showFishInMinigame"),
                () => Desc("showFishInMinigame")
            );
            configApi.AddNumberOption(
                manifest,
                () => this.CatchSpeed,
                val => this.CatchSpeed = val,
                () => Name("catchSpeed"),
                () => Desc("catchSpeed"),
                0f,
                3f
            );
            configApi.AddNumberOption(
                manifest,
                () => this.DrainSpeed,
                val => this.DrainSpeed = val,
                () => Name("drainSpeed"),
                () => Desc("drainSpeed"),
                0f,
                3f
            );
            configApi.AddNumberOption(
                manifest,
                () => this.StreakForIncreasedQuality,
                val => this.StreakForIncreasedQuality = val,
                () => Name("streakForIncreasedQuality"),
                () => Desc("streakForIncreasedQuality"),
                0,
                100
            );
            configApi.AddBoolOption(
                manifest,
                () => this.MaxNormalFishQuality is not null,
                val => this.MaxNormalFishQuality = val ? 0 : null,
                () => Name("maxNormalFishQuality.enabled"),
                () => Desc("maxNormalFishQuality.enabled")
            );
            configApi.AddNumberOption(
                manifest,
                () => this.MaxNormalFishQuality ?? 0,
                val =>
                {
                    if (this.MaxNormalFishQuality is not null)
                    {
                        this.MaxNormalFishQuality = val;
                    }
                },
                () => Name("maxNormalFishQuality"),
                () => Desc("maxNormalFishQuality"),
                0,
                4
            );
            configApi.AddNumberOption(
                manifest,
                () => this.MaxFishQuality,
                val => this.MaxFishQuality = val,
                () => Name("maxFishQuality"),
                () => Desc("maxFishQuality"),
                0,
                3
            );

            // Fish chances
            configApi.AddSectionTitle(
                manifest,
                () => Name("fishChances"),
                () => Desc("fishChances")
            );
            this.FishChances.RegisterOptions(configApi, manifest, translations);
        }
    }
}