#if ZENJECT
namespace AXitUnityTemplate.AssetLoader.Runtime.Installer
{
    using UnityEngine;
    using AXitUnityTemplate.AssetLoader.Runtime.Loader;
    using AXitUnityTemplate.AssetLoader.Runtime.Utilities;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;

    public class AssetLoaderMonoInstaller : MonoBehaviour, IInstaller
    {
        public void Awake() { this.Install(); }

        public void Install()
        {
#if ADDRESSABLES_ASSET_LOADED
            DependencyLocator.AssetLoader = new AddressableAssetLoader();
#else
            DependencyLocator.AssetLoader = new ResourceAssetLoader();
#endif
        }
    }
}
#endif