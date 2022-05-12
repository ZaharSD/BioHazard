
using System;

namespace BioHazard
{
	[Serializable]
	public class Record
	{
		public int Rank { get; set; }
		public float Time { get; set; }
		public DateTime Date { get; set; }
		public int Score { get; set; }

		public Record(int score, float time)
		{
			Date = DateTime.Now;
			Score = score;
			Time = time;
			Rank = 1;
		}
	}
}