
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BioHazard
{
	public class ToolsManager : MonoBehaviour
	{
		[SerializeField] private List<Tool> _tools = new List<Tool>();

		private Tool _currentTool;
		private static ToolsManager _instance;

		private void Awake()
		{
			_instance = this;
		}

		public static Tool Open(string toolName, Vector2 position)
		{
			CloseCurrent();

			var findedTool = _instance._tools.FirstOrDefault(tool => tool.Name.Equals(toolName));

			findedTool.gameObject.SetActive(true);
			findedTool.transform.position = position;

			_instance._currentTool = findedTool;

			return findedTool;
		}

		public static void CloseCurrent()
		{
			_instance._currentTool?.gameObject.SetActive(false);
		}
	}
}