
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class NodeEditWindow : Tool
	{
		[SerializeField] private Dropdown _nodeType;
		[SerializeField] private InputField _maxUnits;
		[SerializeField] private InputField _startUnits;
		[SerializeField] private Dropdown _nodePreset;
		[SerializeField] private Button _delete;

		[SerializeField] private List<NodePreset> _nodePresets = new List<NodePreset>();

		private EditorNode _nodeToEdit;

		private void Awake()
		{
			_nodeType.onValueChanged.AddListener(TypeChanged);
			_startUnits.onValueChanged.AddListener(StartUnitsChanged);
			_maxUnits.onValueChanged.AddListener(MaxUnitsChanged);
			_nodePreset.onValueChanged.AddListener(PresetSelected);
			_delete.onClick.AddListener(Delete);
		}

		public void Init(EditorNode node)
		{
			_nodeToEdit = node;
			_nodeType.value = _nodeType.options.FindIndex(option => option.text.Equals(node.Data.Type.ToString()));
			_maxUnits.text = node.Data.MaxUnits.ToString();
			_startUnits.text = node.Data.StartUnits.ToString();
		}

		private void TypeChanged(int value)
		{
			switch (value)
			{
				case 0:
					_nodeToEdit.ChangeType(NodeType.Neutral);
					break;
				case 1:
					_nodeToEdit.ChangeType(NodeType.Allied);
					break;
				case 2:
					_nodeToEdit.ChangeType(NodeType.Enemy);
					break;
			}
		}

		private void StartUnitsChanged(string startUnits)
		{
			if (string.IsNullOrEmpty(startUnits))
				return;

			_nodeToEdit.ChangeStartUnits(int.Parse(startUnits));
		}

		private void MaxUnitsChanged(string maxUnits)
		{
			if (string.IsNullOrEmpty(maxUnits))
				return;

			_nodeToEdit.ChangeMaxUnits(int.Parse(maxUnits));
		}

		private void Delete()
		{
			LevelEditor.RemoveNode(_nodeToEdit);
		}

		private void PresetSelected(int value)
		{
			var presetName = _nodePreset.options[value].text;
			var data = Preset(presetName);

			_nodeToEdit.ChangeStartUnits(data.StartUnits);
			_nodeToEdit.ChangeMaxUnits(data.MaxUnits);
		}

		private NodeData Preset(string presetName)
		{
			return _nodePresets.FirstOrDefault(preset => preset.Name.Equals(presetName))?.Data;
		}
	}
}