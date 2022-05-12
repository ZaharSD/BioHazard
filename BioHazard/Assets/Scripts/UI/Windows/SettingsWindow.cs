
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class SettingsWindow : Window
	{
		[SerializeField] private Slider _sfx;
		[SerializeField] private Slider _music;
		[SerializeField] private Button _reset;
		[SerializeField] private Button _exit;

		private void Awake()
		{
			_sfx.value = AudioManager.SFXVolume;
			_music.value = AudioManager.MusicVolume;
		}

		private void Start()
		{
			_sfx.onValueChanged.AddListener(AudioManager.SetSFXVolume);
			_music.onValueChanged.AddListener(AudioManager.SetMusicVolume);
			_reset.onClick.AddListener(ResetSliders);

			_exit.onClick.AddListener(BackToMainMenuWindow);
		}

		private void ResetSliders()
		{
			AudioManager.ResetVolume();

			_sfx.value = AudioManager.SFXVolume;
			_music.value = AudioManager.MusicVolume;
		}
		
		private void BackToMainMenuWindow()
		{
			UIManager.Instance.TriggerClose(this);

			var mainMenuWindow = UIManager.Instance.MainMenuWindow;
			UIManager.Instance.TriggerOpen(mainMenuWindow);
		}
	}
}