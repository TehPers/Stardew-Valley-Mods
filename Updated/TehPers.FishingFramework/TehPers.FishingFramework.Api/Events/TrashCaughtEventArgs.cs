﻿using System;
using StardewValley;
using StardewValley.Tools;
using SObject = StardewValley.Object;

namespace TehPers.FishingFramework.Api.Events
{
    /// <summary>
    /// Arguments for the event invoked after the local player catches trash.
    /// </summary>
    public class TrashCaughtEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the farmer that caught the trash. This should be the local player.
        /// </summary>
        public Farmer Catcher { get; }

        /// <summary>
        /// Gets the fishing rod used to catch the trash.
        /// </summary>
        public FishingRod Rod { get; }

        /// <summary>
        /// Gets the <see cref="Item.ParentSheetIndex"/> of the trash that was caught. All trash are instances of <see cref="SObject"/>.
        /// </summary>
        public int TrashIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrashCaughtEventArgs"/> class.
        /// </summary>
        /// <param name="catcher">The farmer that caught the trash.</param>
        /// <param name="rod">The fishing rod used to catch the trash.</param>
        /// <param name="trashIndex">The <see cref="Item.ParentSheetIndex"/> of the trash that was caught.</param>
        public TrashCaughtEventArgs(Farmer catcher, FishingRod rod, int trashIndex)
        {
            this.Catcher = catcher;
            this.Rod = rod;
            this.TrashIndex = trashIndex;
        }
    }
}