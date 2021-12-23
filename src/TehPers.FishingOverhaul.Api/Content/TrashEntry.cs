﻿using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TehPers.Core.Api.Items;
using TehPers.Core.Api.Json;

namespace TehPers.FishingOverhaul.Api.Content
{
    /// <summary>
    /// A trash availability entry.
    /// </summary>
    /// <param name="ItemKey">The item key.</param>
    /// <param name="AvailabilityInfo">The availability information.</param>
    [JsonDescribe]
    public record TrashEntry(
        [property: JsonRequired] NamespacedKey ItemKey,
        AvailabilityInfo AvailabilityInfo
    ) : Entry<AvailabilityInfo>(AvailabilityInfo)
    {
        /// <inheritdoc/>
        public override bool TryCreateItem(
            FishingInfo fishingInfo,
            INamespaceRegistry namespaceRegistry,
            [NotNullWhen(true)] out CaughtItem? item
        )
        {
            if (namespaceRegistry.TryGetItemFactory(this.ItemKey, out var factory))
            {
                item = new(factory.Create());
                return true;
            }

            item = default;
            return false;
        }
    }
}