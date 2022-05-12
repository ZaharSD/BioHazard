
using UnityEngine;

namespace BioHazard
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;

		public MainMenuWindow MainMenuWindow;
		public ExitConfirmWindow ExitConfirmWindow;
		public LoseWindow LoseWindow;
		public PauseWindow PauseWindow;
		public SettingsWindow SettingsWindow;
		public VictoryWindow VictoryWindow;
		public RecordWindow RecordWindow;
		public LevelSelectionWindow LevelSelectionWindow;
		public HUD HUD;
		
		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(gameObject);
			
			TriggerOpen(MainMenuWindow);
		}

		public void TriggerClose(Window window)
		{
			window.Hide();
		}

		public void TriggerOpen(Window window)
		{
			var inactiveWindow = FindOpenedWindow(window);

			if (inactiveWindow != null)
				inactiveWindow.Show();
			else
				Instantiate(window, gameObject.transform);
		}

		private Window FindOpenedWindow(Window window)
		{
			var openedWindows = gameObject.transform.GetComponentsInChildren<Window>(true);

			foreach (var openedWindow in openedWindows)
			{
				if (openedWindow.GetType() == window.GetType())
					return openedWindow;
			}

			return null;
		}
	}
}