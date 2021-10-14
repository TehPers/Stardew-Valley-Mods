﻿using TehPers.Core.Api.DI;
using TehPers.Core.Api.Extensions;
using TehPers.Core.Api.Items;
using TehPers.Core.Integrations.DynamicGameAssets;
using TehPers.Core.Integrations.JsonAssets;
using TehPers.Core.Items;

namespace TehPers.Core.Modules
{
    internal class CoreServicesModule : ModModule
    {
        public override void Load()
        {
            // Services
            this.GlobalProxyRoot.Bind<INamespaceRegistry>()
                .To<NamespaceRegistry>()
                .InSingletonScope();

            // Namespaces
            this.GlobalProxyRoot.Bind<INamespaceProvider>()
                .To<StardewValleyNamespace>()
                .InSingletonScope();
            this.GlobalProxyRoot.Bind<INamespaceProvider>()
                .To<DynamicGameAssetsNamespace>()
                .InSingletonScope();
            this.GlobalProxyRoot.Bind<INamespaceProvider>()
                .To<JsonAssetsNamespace>()
                .InSingletonScope();

            // Mod APIs
            this.BindForeignModApi<IDynamicGameAssetsApi>("spacechase0.DynamicGameAssets")
                .InSingletonScope();
            this.BindForeignModApi<IJsonAssetsApi>("spacechase0.JsonAssets").InSingletonScope();
        }
    }
}