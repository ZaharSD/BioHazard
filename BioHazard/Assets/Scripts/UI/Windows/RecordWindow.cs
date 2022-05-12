using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class RecordWindow : Window
	{
		[SerializeField] private Button _exit;

		private void Start()
		{
			_exit.onClick.AddListener(Exit);
		}

		private void Exit()
		{
			UIManager.Instance.TriggerClose(this);

			var mainMenuWindow = UIManager.Instance.MainMenuWindow;
			UIManager.Instance.TriggerOpen(mainMenuWindow);
		}
	}
}