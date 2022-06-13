﻿using Microsoft.Xna.Framework;

namespace TehPers.Core.Api.Gui
{
    /// <summary>
    /// An event that can update the state of the GUI.
    /// </summary>
    public abstract record GuiEvent
    {
        private GuiEvent() { }

        /// <summary>
        /// A regular update tick.
        /// </summary>
        /// <param name="Time">The current game time.</param>
        public sealed record UpdateTick(GameTime Time) : GuiEvent;

        /// <summary>
        /// A mouse click was received.
        /// </summary>
        /// <param name="Position">The cursor position when receiving the click.</param>
        /// <param name="ClickType">The type of click received.</param>
        public sealed record ReceiveClick(Point Position, ClickType ClickType) : GuiEvent;

        /// <summary>
        /// A custom event.
        /// </summary>
        /// <param name="Data">The event data.</param>
        public sealed record Other(object Data) : GuiEvent;
    }
}
