
using System;
using UnityEngine;

namespace BioHazard
{
	[Serializable]
	public class NodeData
	{
		public Vector2 Position = new Vector2(0.0f, 0.0f);

		public NodeType Type = NodeType.Neutral;

		public int StartUnits = 20;

		public int MaxUnits = 0;
	}
}