#if ADDRESSABLES_ASSET_LOADED
namespace AXitUnityTemplate.AssetLoader.Runtime.Loader
{
#if UNITASK
    using Cysharp.Threading.Tasks;
#else
    using UnityEngine.ResourceManagement.AsyncOperations;
#endif
    using UnityEngine;
    using System.Collections.Generic;
    using UnityEngine.AddressableAssets;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;
    using AXitUnityTemplate.AssetLoader.Runtime.Utilities;
    using UnityEngine.ResourceManagement.ResourceProviders;

    public class AddressableAssetLoader : IAssetLoader
    {
        private readonly Dictionary<string, Object> loadedAssets = new(20);
        
        public IAsyncResult<T> LoadAssetAsync<T>(string key) where T : Object
        {
#if UNITASK
            var task = Addressables.LoadAssetAsync<T>(key);

            return new UniTaskResult<T>(task.ToUniTask());
#else
            var enumerator = LoadAssetIEnumerator();

            return new EnumeratorResult<T>(enumerator);

            IEnumerator<T> LoadAssetIEnumerator()
            {
                var isMonoBehaviour = typeof(MonoBehaviour).IsAssignableFrom(typeof(T));
                AsyncOperationHandle opHandle = isMonoBehaviour
                    ? Addressables.LoadAssetAsync<GameObject>(key)
                    : Addressables.LoadAssetAsync<T>(key);

                // Wait for the operation to complete.
                while (!opHandle.IsDone) yield return null;

                if (opHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    // Check if the asset is a MonoBehaviour and if it is, check if the GameObject has the component.
                    if (isMonoBehaviour)
                    {
                        var gameObject = opHandle.Result as GameObject;
                        if (gameObject && gameObject.TryGetComponent<T>(out var component))
                        {
                            yield return component;
                        }
                        else
                        {
                            Debug.LogError($"GameObject loaded with key: {key} does not have component of type {typeof(T)}.");
                        }
                    }
                    else
                    {
                        yield return opHandle.Result as T;
                    }

                    this.loadedAssets.TryAdd(key, opHandle.Result as Object);
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
            if (this.loadedAssets.Remove(key, out var asset))
            {
                Addressables.Release(asset);

                return;
            }

            Debug.LogError($"Asset with key: {key} is not loaded.");
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string key, bool active = true) { return await Addressables.LoadSceneAsync(key, activateOnLoad: active); }
    }
}
#endif