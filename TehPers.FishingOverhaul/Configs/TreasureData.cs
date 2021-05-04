﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using StardewValley;
using TehPers.Core.Json.Serialization;
using TehPers.FishingOverhaul.Api;

namespace TehPers.FishingOverhaul.Configs {

    [JsonDescribe]
    public class TreasureData : ITreasureData {

        [Description("ID of the first (or only) item")]
        public int Id { get; set; }

        [Description("Weighted chance this range of items will get chosen")]
        public double Chance { get; set; }

        [Description("The minimum amount of this that can be found in one slot")]
        public int MinAmount { get; set; }

        [Description("The maximum amount of this that can be found in one slot")]
        public int MaxAmount { get; set; }

        [Description("The minimum fishing level required to find this")]
        public int MinLevel { get; set; }

        [Description("The maximum fishing level required to find this")]
        public int MaxLevel { get; set; }

        [Description("The size of this range of IDs. For example, if Id = 3 and IdRange = 2, then possible IDs are 2 and 3.")]
        public int IdRange { get; set; }

        [Description("Whether this is a melee weapon")]
        public bool MeleeWeapon { get; set; }

        [Description("Whether this can be found multiple times in one chest")]
        public bool AllowDuplicates { get; set; }

        public TreasureData(int id, double chance, int minAmount = 1, int maxAmount = 1, int minLevel = 0, int maxLevel = 10, int idRange = 1, bool meleeWeapon = false, bool allowDuplicates = true) {
            Id = id;
            Chance = chance;
            MinAmount = Math.Max(1, minAmount);
            MaxAmount = Math.Max(MinAmount, maxAmount);
            MinLevel = minLevel;
            MaxLevel = Math.Max(minLevel, maxLevel);
            IdRange = Math.Max(1, idRange);
            MeleeWeapon = meleeWeapon;
            AllowDuplicates = allowDuplicates;
        }

        public bool IsValid(Farmer who) {
            return who.FishingLevel >= MinLevel && (MaxLevel >= 10 || who.FishingLevel <= MaxLevel);
        }

        public IList<int> PossibleIds() {
            return Enumerable.Range(Id, Math.Max(1, IdRange)).ToList();
        }

        public double GetWeight() {
            return Chance;
        }
    }
}
