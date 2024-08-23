using UnityEngine;

namespace Helpers
{
    public class Layers
    {
        public const string Default = "Default";
		public const string Player = "Player";
		public const string Floor = "Floor";

		private static readonly Layer _playerLayer = new Layer(Player);
		private static readonly Layer _floorLayer = new Layer(Floor);
		private static readonly Layer _defaultLayer = new Layer(Default);
		
		private class Layer
		{
			private readonly string _name;

			private int? _id;

			public int Id
			{
				get
				{
					if (!_id.HasValue)
						_id = LayerMask.NameToLayer(_name);
					return _id.Value;
				}
			}

			public Layer(string name)
			{
				_name = name;
			}
		}
    }
}