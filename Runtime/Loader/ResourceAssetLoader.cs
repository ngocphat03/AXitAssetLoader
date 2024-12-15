#if !ADDRESSABLES_ASSET_LOADED
namespace AXitUnityTemplate.AssetLoader.Runtime.Loader
{
    using UnityEngine;
    using System.Collections.Generic;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;
    using AXitUnityTemplate.AssetLoader.Runtime.Utilities;

    public class ResourceAssetLoader : IAssetLoader
    {
        private readonly Dictionary<string, Object> loadedAssets = new(20);

        public IAsyncResult<T> LoadAssetAsync<T>(string key) where T : Object
        {
#if UNITASK
            var tcs    = new Cysharp.Threading.Tasks.UniTaskCompletionSource<T>();
            var result = Resources.Load<T>(key);
            
            if (result)
            {
                tcs.TrySetResult(result);
                this.loadedAssets.TryAdd(key, result);
            }
            else
            {
                tcs.TrySetException(new System.Exception($"Failed to load asset with key: {key}"));
            }

            return new UniTaskResult<T>(tcs.Task);

#else
            return new EnumeratorResult<T>(LoadAssetIEnumerator());

            IEnumerator<T> LoadAssetIEnumerator()
            {
                var asset = Resources.Load<T>(key);

                if (asset)
                {
                    yield return asset;
                    this.loadedAssets.TryAdd(key, asset);
                }
                else
                {
                    Debug.LogError($"Failed to load asset with key: {key}");
                }
            }
#endif
        }

        public void UnloadAsset(string key)
        {
            if (this.loadedAssets.TryGetValue(key, out var asset))
            {
                Resources.UnloadAsset(asset);
                this.loadedAssets.Remove(key);

                return;
            }

            Debug.LogError($"Asset with key: {key} is not loaded.");
        }
    }
}
#endif