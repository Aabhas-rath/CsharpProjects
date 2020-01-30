namespace Services.Core.Behaviours
{
    public interface IUpdateBehaviour<TEntity> : IPostBehaviour<TEntity> where TEntity:class
    {
        bool Update(TEntity oldEntity, TEntity newEntity);
    }
}
