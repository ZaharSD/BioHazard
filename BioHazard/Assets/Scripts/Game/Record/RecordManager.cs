
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BioHazard
{
	public class RecordManager : MonoBehaviour
	{
		private static Dictionary<string, List<Record>> _records = new Dictionary<string, List<Record>>();

		public static Dictionary<string, List<Record>> Records => _records;

		public void Awake()
		{
			_records = SaveManager.LoadRecords();
		}

		public static void Add(string level, int recruitedUnits, int capturedNodes, float time)
		{
			var score = Mathf.RoundToInt((recruitedUnits * capturedNodes * 60) / time);
			var newRecord = new Record(score, time);

			if (_records.ContainsKey(level))
				_records[level].Add(newRecord);
			else
			{
				_records.Add(level, new List<Record>());
				_records[level].Add(newRecord);
			}
			
			_records[level] = _records[level].OrderByDescending(record => record.Score).ToList();
		}
	}
}