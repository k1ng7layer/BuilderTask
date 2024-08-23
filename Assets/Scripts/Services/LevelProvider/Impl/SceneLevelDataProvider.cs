namespace Services.LevelProvider.Impl
{
    public class SceneLevelDataProvider : ILevelDataProvider
    {
        public SceneLevelDataProvider(ILevelData levelData)
        {
            LevelData = levelData;
        }

        public ILevelData LevelData { get; }
    }
}