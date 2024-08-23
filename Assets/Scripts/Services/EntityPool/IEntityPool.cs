using Entity;

namespace Services.EntityPool
{
    public interface IEntityPool
    {
        GameEntity CreateEntity();
    }
}