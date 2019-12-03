using Models;
using Repository.Interfaces;
using Models.Interfaces;

namespace Repository.Implementations
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(string connectionString) : base(connectionString)
        {
        }

    }
}
