namespace Services.TimeProvider.Impl
{
    public class UnityTimeProvider : ITimeProvider
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}