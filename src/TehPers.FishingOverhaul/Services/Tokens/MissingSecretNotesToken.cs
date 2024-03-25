﻿using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewValley;
using TehPers.Core.Api.Content;
using TehPers.Core.Api.DI;

namespace TehPers.FishingOverhaul.Services.Tokens
{
    internal class MissingSecretNotesToken : MissingNotesToken
    {
        public MissingSecretNotesToken(
            IModHelper helper,
            [ContentSource(ContentSource.GameContent)] IAssetProvider gameAssets
        )
            : base(helper, gameAssets)
        {
        }

        public override IEnumerable<string> GetValues(string? input)
        {
            if (Game1.player is not {secretNotesSeen: { } secretNotesSeen} player)
            {
                return Enumerable.Empty<string>();
            }

            return this.SecretNotes.Keys.Where(id => id < GameLocation.JOURNAL_INDEX)
                .Except(secretNotesSeen)
                .Where(
                    id => !player.Items.Any(item => item != null && item.Name.Equals($"Secret Note #{id}"))
                        && (id != 10 || player.hasOrWillReceiveMail("QiChallengeComplete"))
                )
                .Select(id => id.ToString("G"));
        }
    }
}
