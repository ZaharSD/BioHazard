
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BioHazard
{
	public class EditorNodeView : DragAndDrop, IPointerDownHandler
	{
		[SerializeField] private Image _nodeBubble;
		[SerializeField] private Image _nodeCore;
		[SerializeField] private Text _currentUnits;

		private EditorNode _nodeToEdit;

		public void Init(EditorNode editNode)
		{
			SetType(editNode.Data.Type);
			SetStartUnits(editNode.Data.StartUnits);
			_nodeToEdit = editNode;
		}

		public void SetType(NodeType type)
		{
			switch (type)
			{
				case NodeType.Allied:
					_nodeBubble.color = Color.green;
					_nodeCore.color = Color.green;
					break;
				case NodeType.Enemy:
					_nodeBubble.color = Color.red;
					_nodeCore.color = Color.red;
					break;
				case NodeType.Neutral:
					_nodeBubble.color = Color.gray;
					_nodeCore.color = Color.gray;
					break;
			}
		}

		public void SetStartUnits(int startUnits)
		{
			_currentUnits.text = startUnits.ToString();

			transform.localScale = startUnits == 0
				? new Vector3(1.0f, 1.0f, 1.0f)
				: new Vector3(0.05f * startUnits, 0.05f * startUnits, 0.5f * startUnits);
		}


		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				var nodeEditWindow = ToolsManager.Open("NodeEdit", eventData.position) as NodeEditWindow;
				nodeEditWindow.Init(_nodeToEdit);
			}
		}
	}
}