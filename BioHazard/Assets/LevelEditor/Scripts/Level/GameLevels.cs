
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BioHazard
{
	[CreateAssetMenu(fileName = "Game Levels")]
	public class GameLevels : ScriptableObject
	{
		[SerializeField] private List<LevelData> _levels = new List<LevelData>();

		public LevelData this[string levelName] => _levels.FirstOrDefault(level => level.Name.Equals(levelName));
		public LevelData this[int levelIndex] => _levels[levelIndex];
		public List<LevelData> LevelsData => _levels;
		public int Count => _levels.Count;
		public int LastIndex => _levels.Count - 1;

		public void Add(LevelData data)
		{
			_levels.Add(data);
		}

		public void Remove(LevelData data)
		{
			_levels.Remove(data);

			for (var i = 0; i < _levels.Count; i++)
				_levels[i].Name = $"Level {i}";
		}

		public bool Exists(string levelName)
		{
			return _levels.Exists(level => level.Name == levelName);
		}

		public int IndexOf(string levelName)
		{
			return _levels.FindIndex(level => level.Name.Equals(levelName));
		}
	}
}