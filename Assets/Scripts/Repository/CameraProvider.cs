using Entity;

namespace Repository
{
    public class CameraProvider
    {
        public GameEntity Camera { get; private set; }

        public void SetCamera(GameEntity gameEntity)
        {
            Camera = gameEntity;
        }
    }
}