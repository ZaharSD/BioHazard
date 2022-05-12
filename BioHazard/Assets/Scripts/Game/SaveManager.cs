
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BioHazard
{
	public class SaveManager : MonoBehaviour
	{
		public static string PathToSave => Application.persistentDataPath + "/records.records";

		public static void SaveProgress(int passedLevelIndex, Dictionary<string, List<Record>> records)
		{
			PlayerPrefs.SetInt("PassedLevels", passedLevelIndex);

			var binFormatter = new BinaryFormatter();
			using var memoryStream = new MemoryStream();
			binFormatter.Serialize(memoryStream, records);

			File.WriteAllBytes(PathToSave, memoryStream.ToArray());
		}

		public static Dictionary<string, List<Record>> LoadRecords()
		{
			if (!File.Exists(PathToSave))
				return new Dictionary<string, List<Record>>();

			var binFormatter = new BinaryFormatter();
			var bytes = File.ReadAllBytes(PathToSave);

			using var memoryStream = new MemoryStream(bytes);

			var records = (Dictionary<string, List<Record>>) binFormatter.Deserialize(memoryStream);

			return records;
		}
	}
}