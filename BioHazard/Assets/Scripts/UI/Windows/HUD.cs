

using System;
using BioHazard;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Window
{
	[SerializeField] private Button _resetLevel;
	[SerializeField] private Button _exit;

	public static event Action StopGame;
	public static event Action DeactivateEnemyAI;
	public static event Action ResetCurrentLevel;

	private void Start()
	{
		_resetLevel.onClick.AddListener(ResetLevel);
		_exit.onClick.AddListener(Exit);
	}

	private void ResetLevel()
	{
		StopGame?.Invoke();
		ResetCurrentLevel?.Invoke();
	}

	private void Exit()
	{
		DeactivateEnemyAI?.Invoke();
		StopGame?.Invoke();
		
		UIManager.Instance.TriggerClose(this);

		var levelSelectionWindow = UIManager.Instance.LevelSelectionWindow;
		UIManager.Instance.TriggerOpen(levelSelectionWindow);
	}
}