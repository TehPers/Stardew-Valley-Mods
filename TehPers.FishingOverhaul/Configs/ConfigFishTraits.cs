﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using StardewModdingAPI;
using TehPers.Core.Json.Serialization;
using TehPers.FishingOverhaul.Api.Enums;

namespace TehPers.FishingOverhaul.Configs {
    [JsonDescribe]
    public class ConfigFishTraits {
        [Description("The traits for each fish.")]
        public Dictionary<int, FishTraits> FishTraits { get; set; }

        public void PopulateData() {
            ModFishing.Instance.Monitor.Log("Automatically populating fishTraits.json with data from Fish.xnb", LogLevel.Info);
            ModFishing.Instance.Monitor.Log("NOTE: If this file is modded, the config will reflect the changes!", LogLevel.Info);

            var fishDict = ModFishing.Instance.Helper.Content.Load<Dictionary<int, string>>(@"Data\Fish.xnb", ContentSource.GameContent);
            FishTraits ??= new Dictionary<int, FishTraits>();
            var possibleFish = (from locationKV in ModFishing.Instance.FishConfig.PossibleFish
                                             from fishKV in locationKV.Value
                                             select fishKV.Key).Distinct();

            // Loop through each possible fish
            foreach (var fish in possibleFish) {
                try {
                    if (!fishDict.TryGetValue(fish, out var rawData))
                        continue;

                    var data = rawData.Split('/');

                    // Get difficulty
                    int.TryParse(data[1], out var difficulty);

                    // Get motion type
                    var motionTypeName = data[2].ToLower();
                    FishMotionType motionType;
                    switch (motionTypeName) {
                        case "mixed":
                            motionType = FishMotionType.MIXED;
                            break;
                        case "dart":
                            motionType = FishMotionType.DART;
                            break;
                        case "smooth":
                            motionType = FishMotionType.SMOOTH;
                            break;
                        case "sinker":
                            motionType = FishMotionType.SINKER;
                            break;
                        case "floater":
                            motionType = FishMotionType.FLOATER;
                            break;
                        default:
                            motionType = FishMotionType.MIXED;
                            break;
                    }

                    // Get size
                    var minSize = Convert.ToInt32(data[3]);
                    var maxSize = Convert.ToInt32(data[4]);

                    // Add trait
                    FishTraits.Add(fish, new FishTraits {
                        Difficulty = difficulty,
                        MinSize = minSize,
                        MaxSize = maxSize,
                        MotionType = motionType
                    });
                } catch (Exception) {
                    ModFishing.Instance.Monitor.Log($"Failed to generate traits for {fish}, vanilla traits will be used.", LogLevel.Warn);
                }
            }
        }
    }
}
