
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class ExitConfirmWindow : Window
	{
		[SerializeField] private Button _yes;
		[SerializeField] private Button _no;

		private void Start()
		{
			_yes.onClick.AddListener(Exit);
			_no.onClick.AddListener(ReturnToMenu);
		}

		private void Exit()
		{
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}

		private void ReturnToMenu()
		{
			UIManager.Instance.TriggerClose(this);

			var mainMenuWindow = UIManager.Instance.MainMenuWindow;
			UIManager.Instance.TriggerOpen(mainMenuWindow);
		}
	}
}