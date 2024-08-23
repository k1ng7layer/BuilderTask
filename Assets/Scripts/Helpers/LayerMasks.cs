using UnityEngine;

namespace Helpers
{
    public static class LayerMasks
    {
        private static readonly Mask PlayerMask = new Mask(Layers.Player);
		private static readonly Mask DefaultMask = new Mask(Layers.Default);
		private static readonly Mask FloorMask = new Mask(Layers.Floor);
		private static readonly Mask BuildingSurfaceMask = new Mask(Layers.BuildingSurface);
		
		public static int Player => PlayerMask.Value;
		public static int Default => DefaultMask.Value;
		public static int Floor => FloorMask.Value;
		public static int BuildingSurface => BuildingSurfaceMask.Value;

		// public static Mask CreateMasks()
		// {
		// 	
		// }
		
		public class Mask
		{
			private readonly string[] _layerNames;

			private int? _value;

			public Mask(params string[] layerNames)
			{
				_layerNames = layerNames;
			}

			public int Value
			{
				get
				{
					if (!_value.HasValue)
						_value = LayerMask.GetMask(_layerNames);
					return _value.Value;
				}
			}
		}
    }
}