namespace EasyAgenda.Data.Contracts
{
    public interface IManipulation<T> where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
