
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class ToolsPanel : MonoBehaviour
	{
		[SerializeField] private Dropdown _levelsDropdown;
		[SerializeField] private Button _newLevelButton;
		[SerializeField] private Button _deleteLevelButton;
		[SerializeField] private Button _saveLevelButton;

		private void Awake()
		{
			_levelsDropdown.onValueChanged.AddListener(SelectLevel);
			_saveLevelButton.onClick.AddListener(LevelEditor.SaveLevel);
			_newLevelButton.onClick.AddListener(CreateLevel);
			_deleteLevelButton.onClick.AddListener(LevelEditor.DeleteLevel);

			LevelEditor.OnLevelsChanged += RefreshDropdownLevels;
		}

		private void Start()
		{
			RefreshDropdownLevels();
		}

		private void RefreshDropdownLevels()
		{
			_levelsDropdown.ClearOptions();
			_saveLevelButton.gameObject.SetActive(false);
			
			if (LevelEditor.Levels.Count > 0)
			{
				foreach (var levelData in LevelEditor.Levels.LevelsData)
					_levelsDropdown.options.Add(new Dropdown.OptionData {text = levelData.Name});

				_levelsDropdown.value = _levelsDropdown.options.IndexOf(_levelsDropdown.options.Last());
				_levelsDropdown.captionText.text = _levelsDropdown.options[_levelsDropdown.value].text;
				_saveLevelButton.gameObject.SetActive(true);
			}
		}

		private void SelectLevel(int value)
		{
			LevelEditor.LoadLevel(value);
		}

		private void CreateLevel()
		{
			LevelEditor.CreateLevel();
		}
	}
}