using Entity;
using Services.Factories.Entity;

namespace Services.EntityPool.Impl
{
    public class EntityPool : IEntityPool
    {
        private readonly IEntityFactory _entityFactory;

        public EntityPool(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }
        
        public GameEntity CreateEntity()
        {
            return _entityFactory.Create();
        }
    }
}