namespace AXitUnityTemplate.AssetLoader.Runtime.Scripts.Interfaces
{
    using UnityEngine;

    public interface IAssetLoader
    {
        public IAsyncResult<T> LoadAssetAsync<T>(string key) where T : Object;

        public void UnloadAsset(string key);
    }
}