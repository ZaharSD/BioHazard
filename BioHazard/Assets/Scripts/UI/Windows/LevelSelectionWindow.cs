
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class LevelSelectionWindow : Window
	{
		[SerializeField] private Button _level1;
		[SerializeField] private Button _level2;
		[SerializeField] private Button _level3;
		[SerializeField] private Button _level4;
		[SerializeField] private Button _level5;
		[SerializeField] private Button _exit;

		[SerializeField] private GameLevels _levels;

		public static event Action<NodeData> LoadLevel;
		public static event Action ActivateEnemyAI;

		private int _currentLevel;

		private void Start()
		{
			HUD.ResetCurrentLevel += OnRestartCurrentLevel;
			
			_exit.onClick.AddListener(Exit);
			
			_level1.onClick.AddListener(delegate { SelectLevel(0); });
			_level2.onClick.AddListener(delegate { SelectLevel(1); });
			_level3.onClick.AddListener(delegate { SelectLevel(2); });
			_level4.onClick.AddListener(delegate { SelectLevel(3); });
			_level5.onClick.AddListener(delegate { SelectLevel(4); });
		}
		
		private void Exit()
		{
			UIManager.Instance.TriggerClose(this);

			var mainMenuWindow = UIManager.Instance.MainMenuWindow;
			UIManager.Instance.TriggerOpen(mainMenuWindow);
		}
		
		private void SelectLevel(int selectedLevel = 1)
		{
			_currentLevel = selectedLevel;
			
			UIManager.Instance.TriggerClose(this);
			
			var hud = UIManager.Instance.HUD;
			UIManager.Instance.TriggerOpen(hud);
			
			var listNode = _levels.LevelsData[_currentLevel].Nodes;

			foreach (var node in listNode)
			{
				LoadLevel?.Invoke(node);
			}
			
			ActivateEnemyAI?.Invoke();
		}

		private void OnRestartCurrentLevel()
		{
			UIManager.Instance.TriggerClose(this);
			
			var hud = UIManager.Instance.HUD;
			UIManager.Instance.TriggerOpen(hud);
			
			var listNode = _levels.LevelsData[_currentLevel].Nodes;

			foreach (var node in listNode)
			{
				LoadLevel?.Invoke(node);
			}
			
			ActivateEnemyAI?.Invoke();
		}
	}
}
