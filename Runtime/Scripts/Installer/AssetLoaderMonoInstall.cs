namespace AXitUnityTemplate.AssetLoader.Runtime.Scripts.Installer
{
    using UnityEngine;
    using AXitUnityTemplate.AssetLoader.Runtime.Scripts.Loader;
    using AXitUnityTemplate.AssetLoader.Runtime.Scripts.Utilities;
    using AXitUnityTemplate.AssetLoader.Runtime.Scripts.Interfaces;

    public class AssetLoaderMonoInstall : MonoBehaviour, IInstaller
    {
        public void Awake() { this.InstallBindings(); }

        public void InstallBindings()
        {
#if ADDRESSABLES_ASSET_LOADED
            DependencyLocator.AssetLoader = new AddressableAssetLoader();
#else
            DependencyLocator.AssetLoader = new ResourceAssetLoader();
#endif
        }
    }
}