namespace Core
{
    public interface IPool<T>
    {
        public void Add(T obj);
        public T Get();
    }
}