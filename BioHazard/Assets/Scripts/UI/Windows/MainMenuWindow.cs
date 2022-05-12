
using BioHazard;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
	[SerializeField] private Button _play;
	[SerializeField] private Button _settings;
	[SerializeField] private Button _exit;

	private void Start()
	{
		_play.onClick.AddListener(Play);
		_settings.onClick.AddListener(OpenSettings);
		_exit.onClick.AddListener(Exit);
	}

	private void Play()
	{
		UIManager.Instance.TriggerClose(this);

		var levelSelectionWindow = UIManager.Instance.LevelSelectionWindow;
		UIManager.Instance.TriggerOpen(levelSelectionWindow);
	}

	private void OpenSettings()
	{
		UIManager.Instance.TriggerClose(this);

		var settingsWindow = UIManager.Instance.SettingsWindow;
		UIManager.Instance.TriggerOpen(settingsWindow);
	}

	private void Exit()
	{
		UIManager.Instance.TriggerClose(this);

		var exitConfirmWindow = UIManager.Instance.ExitConfirmWindow;
		UIManager.Instance.TriggerOpen(exitConfirmWindow);
	}
}