
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
	[SerializeField] private Button _resume;
	[SerializeField] private Button _restart;
	[SerializeField] private Button _exit;

	private void Start()
	{
		_resume.onClick.AddListener(Resume);
		_restart.onClick.AddListener(Restart);
		_exit.onClick.AddListener(Exit);
	}

	private void Resume()
	{
		// убрать паузу
	}

	private void Restart()
	{
		// рестарт
	}

	private void Exit()
	{
		// вернуться к выбору уровней
	}
}