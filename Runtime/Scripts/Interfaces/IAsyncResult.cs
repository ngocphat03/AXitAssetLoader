namespace AXitUnityTemplate.AssetLoader.Runtime.Scripts.Interfaces
{

    public interface IAsyncResult<T>
    {
        public T GetResult();

        public bool IsCompleted { get; }

#if UNITASK
        public Cysharp.Threading.Tasks.UniTask<T> ToUniTask();
#else
        public System.Collections.IEnumerator ToEnumerator();
#endif
    }
}