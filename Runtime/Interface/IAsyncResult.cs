namespace AXitUnityTemplate.AssetLoader.Runtime.Interface
{

    public interface IAsyncResult<out T>
    {
        public T GetResult();

        public bool IsCompleted { get; }

#if UNITASK
        public Cysharp.Threading.Tasks.UniTask<T> ToUniTask();
#else
        public System.Collections.IEnumerator ToCoroutine(System.Action<T> onComplete = null)
        {
            return IAsyncResult<T>.ToCoroutineInternal(this, onComplete);
        }

        private static System.Collections.IEnumerator ToCoroutineInternal(IAsyncResult<T> asyncResult, System.Action<T> onComplete = null)
        {
            while (!asyncResult.IsCompleted)
            {
                yield return null;
            }

            onComplete?.Invoke(asyncResult.GetResult());
        }
#endif
    }
}