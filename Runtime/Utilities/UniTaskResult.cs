#if UNITASK
namespace AXitUnityTemplate.AssetLoader.Runtime.Utilities
{
    using Cysharp.Threading.Tasks;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;

    public class UniTaskResult<T> : IAsyncResult<T>
    {
        private readonly UniTask<T> _task;

        public UniTaskResult(UniTask<T> task) { this._task = task; }

        public T GetResult() { return this._task.GetAwaiter().GetResult(); }

        public bool IsCompleted => this._task.Status == UniTaskStatus.Succeeded;

        public UniTask<T> ToUniTask() { return this._task; }
    }
}
#endif