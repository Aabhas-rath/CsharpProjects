namespace Services.Core.Behaviours
{
    public interface IDeleteBehaviour<TEntity> where TEntity : class
    {
        bool Delete(TEntity entity);
        bool DeleteAll();
    }
}
