﻿using System;
using System.Collections.Generic;
using System.Linq;
using ContentPatcher;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using StardewValley.SpecialOrders;
using TehPers.Core.Api.Setup;
using TehPers.FishingOverhaul.Services.Tokens;
using System.Reflection;
using TehPers.Core.Api.Items;
using StardewValley.Monsters;

namespace TehPers.FishingOverhaul.Services.Setup
{
    internal sealed class ContentPatcherSetup : ISetup
    {
        private readonly IManifest manifest;
        private readonly IContentPatcherAPI contentPatcherApi;
        private readonly MissingSecretNotesToken missingSecretNotesToken;
        private readonly MissingJournalScrapsToken missingJournalScrapsToken;

        public ContentPatcherSetup(
            IManifest manifest,
            IContentPatcherAPI contentPatcherApi,
            MissingSecretNotesToken missingSecretNotesToken,
            MissingJournalScrapsToken missingJournalScrapsToken
        )
        {
            this.manifest = manifest ?? throw new ArgumentNullException(nameof(manifest));
            this.contentPatcherApi = contentPatcherApi
                ?? throw new ArgumentNullException(nameof(contentPatcherApi));
            this.missingSecretNotesToken = missingSecretNotesToken
                ?? throw new ArgumentNullException(nameof(missingSecretNotesToken));
            this.missingJournalScrapsToken = missingJournalScrapsToken
                ?? throw new ArgumentNullException(nameof(missingJournalScrapsToken));
        }

        public void Setup()
        {
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "BooksFound",
                new BooksFoundToken()
            );
            this.contentPatcherApi.RegisterToken(this.manifest, "HasItem", new HasItemToken());
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "SpecialOrderRuleActive",
                new MaybeReadyToken(ContentPatcherSetup.GetSpecialOrderRuleActive)
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "MissingSecretNotes",
                this.missingSecretNotesToken
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "MissingJournalScraps",
                this.missingJournalScrapsToken
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "RandomGoldenWalnuts",
                new MaybeReadyToken(ContentPatcherSetup.GetRandomGoldenWalnuts)
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "TidePoolGoldenWalnut",
                new MaybeReadyToken(ContentPatcherSetup.GetTidePoolGoldenWalnut)
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "ActiveBait",
                new MaybeReadyToken(ContentPatcherSetup.GetActiveBait)
            );
            this.contentPatcherApi.RegisterToken(
                this.manifest,
                "ActiveTackle",
                new MaybeReadyToken(ContentPatcherSetup.GetActiveTackle)
            );
        }

        private static IEnumerable<string>? GetSpecialOrderRuleActive()
        {
            if (Game1.player is not { } player)
            {
                return null;
            }

            if (player is not { team.specialOrders: { } specialOrders })
            {
                return Enumerable.Empty<string>();
            }

            return specialOrders.SelectMany(
                    specialOrder =>
                    {
                        if (specialOrder.questState.Value is not SpecialOrderStatus.InProgress)
                        {
                            return Enumerable.Empty<string>();
                        }

                        if (specialOrder.specialRule.Value is not { } specialRule)
                        {
                            return Enumerable.Empty<string>();
                        }

                        return specialRule.Split(
                            ',',
                            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
                        );
                    }
                )
                .OrderBy(val => val, StringComparer.OrdinalIgnoreCase);
        }

        private static IEnumerable<string>? GetRandomGoldenWalnuts()
        {
            if (Game1.player is not { } player)
            {
                return null;
            }

            if (player is not { team: { limitedNutDrops: { } limitedNutDrops } })
            {
                return Enumerable.Empty<string>();
            }

            return limitedNutDrops.TryGetValue("IslandFishing", out var fishingNuts)
                ? new[] {fishingNuts.ToString("G")}
                : new[] {"0"};
        }

        private static IEnumerable<string>? GetTidePoolGoldenWalnut()
        {
            if (Game1.player is not { } player)
            {
                return null;
            }

            if (player is not { team: { } team })
            {
                return Enumerable.Empty<string>();
            }

            return team.collectedNutTracker.Contains("StardropPool")
                ? new[] {"true"}
                : new[] {"false"};
        }

        private static IEnumerable<string>? GetActiveBait()
        {
            if (Game1.player is not {CurrentItem: FishingRod rod})
            {
                return null;
            }

            var bait = rod.GetBait();
            if (bait is null)
            {
                return Enumerable.Empty<string>();
            }

            return new[] { NamespacedKey.SdvObject(bait.ItemId).ToString() };
        }

        private static IEnumerable<string>? GetActiveTackle()
        {
            if (Game1.player is not {CurrentItem: FishingRod rod})
            {
                return null;
            }

            var tackleList = rod.GetTackle();
            return tackleList == null
            ? Enumerable.Empty<string>()
                : tackleList.Select(tackle => NamespacedKey.SdvObject(tackle.ItemId).ToString());
        }
    }
}
