namespace Services.Core.Behaviours
{
    public interface IPostBehaviour<TEntity> where TEntity : class
    {
        int Post(TEntity entity);
    }
}
