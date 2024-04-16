namespace Utkarsh.UnityCore.Pooling
{
    public interface IPooledObject<T>
    {
        void RegisterToPool(IPool<T> pool);
    }
}