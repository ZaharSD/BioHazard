
using UnityEngine;

namespace BioHazard
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private GameLevels _levels;

		private string _currentLevel;
		private static LevelManager _instance;

		public static int LevelsCount => _instance._levels.Count;
		public static int PassedLevels => PlayerPrefs.GetInt("PassedLevels");

		private void Awake()
		{
			_instance = this;
		}

		public static LevelData LoadLevel(int levelIndex)
		{
			_instance._currentLevel = _instance._levels[levelIndex].Name;
			
			return _instance._levels[levelIndex];
		}

		public static void ExitLevel()
		{
			_instance._currentLevel = null;
		}

		private static void PassLevel()
		{
			_instance._currentLevel = null;

			var levelIndex = _instance._levels.IndexOf(_instance._currentLevel);

			// в метод попадут количество юнитов и захваченные ноды из Game Manager и время из TimeManager
			// RecordManager.Add(_instance._currentLevel, );

			SaveManager.SaveProgress(levelIndex, RecordManager.Records);
		}
	}
}