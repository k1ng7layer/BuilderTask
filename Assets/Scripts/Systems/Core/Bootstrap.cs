using System.Collections.Generic;
using Zenject;

namespace Systems.Core
{
    public class Bootstrap : ITickable, 
        IInitializable
    {
        private readonly List<IUpdateSystem> _updateSystems = new();
        private readonly List<IInitializeSystem> _initializeSystems = new();
        
        public Bootstrap(List<ISystem> systems, List<IInitializeSystem> initializeSystems)
        {
            foreach (var system in systems)
            {
                if (system is IUpdateSystem updateSystem)
                    _updateSystems.Add(updateSystem);
                
                if (system is IInitializeSystem initializeSystem)
                    _initializeSystems.Add(initializeSystem);
            }
        }
        
        public void Initialize()
        {
            foreach (var initializeSystem in _initializeSystems)
            {
                initializeSystem.Initialize();
            }
        }
        
        public void Tick()
        {
            foreach (var updateSystem in _updateSystems)
            {
                updateSystem.Update();
            }
        }
    }
}