#if ZENJECT
namespace AXitUnityTemplate.AssetLoader.Runtime.Installer
{
    using Zenject;
    using AXitUnityTemplate.AssetLoader.Runtime.Loader;
    using AXitUnityTemplate.AssetLoader.Runtime.Utilities;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;
    using BaseInstall = AXitUnityTemplate.AssetLoader.Runtime.Interface.IInstaller;

    public class AssetLoaderZenjectInstaller : Installer<AssetLoaderZenjectInstaller>, BaseInstall
    {
        public override void InstallBindings() { this.Install(); }

        public void Install()
        {
#if ADDRESSABLES_ASSET_LOADED
            this.Container.Bind<IAssetLoader>().To<AddressableAssetLoader>().AsCached();
            DependencyLocator.AssetLoader = this.Container.Resolve<IAssetLoader>();
#else
            this.Container.Bind<IAssetLoader>().To<ResourceAssetLoader>().AsCached();
            DependencyLocator.AssetLoader = this.Container.Resolve<IAssetLoader>();
#endif
        }
    }
}
#endif