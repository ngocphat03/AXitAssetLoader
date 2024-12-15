#if !UNITASK
namespace AXitUnityTemplate.AssetLoader.Runtime.Utilities
{
    using System.Collections;
    using AXitUnityTemplate.AssetLoader.Runtime.Interface;

    public class EnumeratorResult<T> : IAsyncResult<T>
    {
        private readonly IEnumerator _enumerator;

        public EnumeratorResult(IEnumerator enumerator) { this._enumerator = enumerator; }

        public T GetResult() => this._enumerator.Current is T result ? result : default;

        public bool IsCompleted => this._enumerator.MoveNext() == false;

        public IEnumerator ToEnumerator() => this._enumerator;
    }
}
#endif