
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class RecordItem : MonoBehaviour
	{
		[SerializeField] private Text _rank;
		[SerializeField] private Text _time;
		[SerializeField] private Text _date;
		[SerializeField] private Text _score;

		public void SetInfo(Record record)
		{
			_rank.text = record.Rank.ToString();
			_time.text = record.Time.ToString();
			_date.text = record.Date.ToString();
			_score.text = record.Score.ToString();
		}
	}
}