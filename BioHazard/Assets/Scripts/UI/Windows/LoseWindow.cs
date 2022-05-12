
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class LoseWindow : Window
	{
		[SerializeField] private Text _score;
		[SerializeField] private Text _time;
		
		[SerializeField] private Button _restart;
		[SerializeField] private Button _exit;

		private void Start()
		{
			_restart.onClick.AddListener(Restart);
			_exit.onClick.AddListener(Exit);
		}

		private void Restart()
		{
			// рестарт уровня
		}

		private void Exit()
		{
			// выход в меню уровней
		}

		public void SetScore(int score)
		{
			_score.text = score.ToString();
		}

		public void SetTime(float time)
		{
			_time.text = time.ToString();
		}

		public void SetRecord(Record record)
		{
			SetScore(record.Score);
			SetTime(record.Time);
		}
	}
}