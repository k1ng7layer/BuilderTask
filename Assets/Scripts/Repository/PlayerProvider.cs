using Entity;

namespace Repository
{
    public class PlayerProvider
    {
        public PlayerEntity Player { get; private set; }
        
        public void Set(PlayerEntity playerEntity)
        {
            Player = playerEntity;
        }
    }
}