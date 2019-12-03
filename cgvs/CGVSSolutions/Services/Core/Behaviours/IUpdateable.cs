namespace Services.Core.Behaviours
{
    interface IUpdateable<TEntity> : IPostBehaviour<TEntity> where TEntity:class
    {
        bool Update(TEntity oldEntity, TEntity newEntity);
    }
}
