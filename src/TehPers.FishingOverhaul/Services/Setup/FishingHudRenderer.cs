﻿using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using TehPers.Core.Api.Items;
using TehPers.Core.Api.Setup;
using TehPers.Core.Gui.Api;
using TehPers.Core.Gui.Api.Components;
using TehPers.Core.Gui.Api.Extensions;
using TehPers.Core.Gui.Api.Guis;
using TehPers.FishingOverhaul.Api;
using TehPers.FishingOverhaul.Config;

namespace TehPers.FishingOverhaul.Services.Setup
{
    internal sealed class FishingHudRenderer : ISetup, IDisposable
    {
        private readonly IModHelper helper;
        private readonly IFishingApi fishingApi;
        private readonly HudConfig hudConfig;
        private readonly INamespaceRegistry namespaceRegistry;
        private readonly ICoreGuiApi coreGuiApi;

        public FishingHudRenderer(
            IModHelper helper,
            IFishingApi fishingApi,
            HudConfig hudConfig,
            INamespaceRegistry namespaceRegistry,
            ICoreGuiApi coreGuiApi
        )
        {
            this.helper = helper ?? throw new ArgumentNullException(nameof(helper));
            this.fishingApi = fishingApi ?? throw new ArgumentNullException(nameof(fishingApi));
            this.hudConfig = hudConfig ?? throw new ArgumentNullException(nameof(hudConfig));
            this.namespaceRegistry = namespaceRegistry
                ?? throw new ArgumentNullException(nameof(namespaceRegistry));
            this.coreGuiApi = coreGuiApi ?? throw new ArgumentNullException(nameof(coreGuiApi));
        }

        public void Setup()
        {
            this.helper.Events.Display.RenderedHud += this.RenderFishingHud;

            // TODO: remove this
            this.helper.Events.Input.ButtonPressed += (sender, args) =>
            {
                if (args.Button != SButton.Y || Game1.activeClickableMenu is not null)
                {
                    return;
                }

                Game1.InUIMode(
                    () => Game1.activeClickableMenu =
                        this.CreateTestMenu(this.coreGuiApi.GuiBuilder)
                );
            };
        }

        private IClickableMenu CreateTestMenu(IGuiBuilder ui)
        {
            return new TestMenu(this.helper).ToMenu(ui, this.helper);
        }

        public void Dispose()
        {
            this.helper.Events.Display.RenderedHud -= this.RenderFishingHud;
        }

        private void RenderFishingHud(object? sender, RenderedHudEventArgs e)
        {
            // Check if HUD should be rendered
            var farmer = Game1.player;
            if (!this.hudConfig.ShowFishingHud
                || Game1.eventUp
                || farmer.CurrentTool is not FishingRod)
            {
                return;
            }

            // Draw the fishing HUD
            var component = new FishingHud(
                this.fishingApi,
                this.helper,
                this.hudConfig,
                this.namespaceRegistry,
                this.coreGuiApi.GuiBuilder,
                farmer
            );
            var constraints = component.GetConstraints();
            component.Handle(
                new GuiEvent.Draw(e.SpriteBatch),
                new(
                    this.hudConfig.TopLeftX,
                    this.hudConfig.TopLeftY,
                    (int)Math.Ceiling(constraints.MinSize.Width),
                    (int)Math.Ceiling(constraints.MinSize.Height)
                )
            );
        }
    }
}
