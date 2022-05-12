
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class LevelEditWindow : Tool
	{
		[SerializeField] private Button _createNode;

		private void Awake()
		{
			_createNode.onClick.AddListener(Create);
		}

		private void Create()
		{
			var preset = new NodeData
			{
				Position = Input.mousePosition,
			};
			
			LevelEditor.CreateNode(preset);
		}
	}
}