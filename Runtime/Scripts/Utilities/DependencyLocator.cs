namespace AXitUnityTemplate.AssetLoader.Runtime.Scripts.Utilities
{
    using UnityEngine;
    using AXitUnityTemplate.AssetLoader.Runtime.Scripts.Interfaces;

    public static class DependencyLocator
    {
        private static IAssetLoader assetLoader;

        public static IAssetLoader AssetLoader
        {
            get
            {
#if USE_DI
            if (_assetLoader == null && DIContainer.HasBinding<IAssetLoader>())
            {
                _assetLoader = DIContainer.Resolve<IAssetLoader>();
            }
#endif
                if (DependencyLocator.assetLoader != null) return DependencyLocator.assetLoader;
                
                Debug.LogError("AssetLoader not initialized!!!");
                return null;
            }
            set
            {
                if (DependencyLocator.assetLoader != null)
                {
                    Debug.LogWarning("AssetLoader is already set. Overriding with new instance.");
                }
                DependencyLocator.assetLoader = value;
            }
        }
    }
}