namespace Services.Collision
{
    public interface ISurfaceCollisionService
    {
        bool CheckCollisionByHash(int itemHash);
        void AddCollision(int hash, int itemHash);
        void RemoveCollision(int hash, int itemHash);
    }
}