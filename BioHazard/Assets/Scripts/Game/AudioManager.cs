
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BioHazard
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSourceSFX;
		[SerializeField] private AudioSource _audioSourceMusic;
		[SerializeField] private AudioMixer _audioMixer;
		private readonly Dictionary<string, AudioClip> _audio = new Dictionary<string, AudioClip>();

		private static AudioManager _instance;

		public static float DefaultVolume => -30.0f;
		public static float SFXVolume => PlayerPrefs.GetFloat("SFX");
		public static float MusicVolume => PlayerPrefs.GetFloat("Music");

		private void Awake()
		{
			_instance = this;

			var audio = Resources.LoadAll<AudioClip>("Audio/");
			foreach (var audioClip in audio)
				_audio.Add(audioClip.name, audioClip);
		}

		public static void PlaySound(string sound)
		{
			_instance._audioSourceSFX.PlayOneShot(_instance._audio[sound]);
		}

		public static void PlayMusic(string music, bool loopable)
		{
			_instance._audioSourceMusic.clip = _instance._audio[music];
			_instance._audioSourceMusic.loop = loopable;
			_instance._audioSourceMusic.Play();
		}

		public static void PauseMusic()
		{
			_instance._audioSourceMusic.Pause();
		}

		public static void UnpauseMusic()
		{
			_instance._audioSourceMusic.UnPause();
		}

		public static void SetSFXVolume(float volume)
		{
			_instance._audioMixer.SetFloat("SFX", volume);

			PlayerPrefs.SetFloat("SFX", volume);
		}

		public static void SetMusicVolume(float volume)
		{
			_instance._audioMixer.SetFloat("Music", volume);

			PlayerPrefs.SetFloat("Music", volume);
		}

		public static void ResetVolume()
		{
			_instance._audioMixer.SetFloat("Music", DefaultVolume);
			_instance._audioMixer.SetFloat("SFX", DefaultVolume);
			
			PlayerPrefs.SetFloat("SFX", DefaultVolume);
			PlayerPrefs.SetFloat("Music", DefaultVolume);
		}
	}
}