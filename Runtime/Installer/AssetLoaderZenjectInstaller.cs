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
            DependencyLocator.AssetLoader = new AddressableAssetLoader();
            this.Container.Bind<IAssetLoader>().To<AddressableAssetLoader>();
#else
            DependencyLocator.AssetLoader = new ResourceAssetLoader();
            this.Container.Bind<IAssetLoader>().To<ResourceAssetLoader>();
#endif
        }
    }
}
#endif