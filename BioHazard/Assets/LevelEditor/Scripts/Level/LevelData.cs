
using System;
using System.Collections.Generic;

namespace BioHazard
{
	[Serializable]
	public class LevelData
	{
		public string Name = "Level";
		public List<NodeData> Nodes = new List<NodeData>();
	}
}