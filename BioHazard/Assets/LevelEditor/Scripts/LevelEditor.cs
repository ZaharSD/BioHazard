
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BioHazard
{
	public class LevelEditor : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
	{
		[SerializeField] private GameLevels _gameLevels;
		[SerializeField] private EditorNodeView _nodePrefab;
		[SerializeField] private GameObject _editorLayer;

		private List<EditorNode> _currentNodes = new List<EditorNode>();
		private LevelData _currentLevel;

		private static LevelEditor _instance;
		
		public static bool IsEditable => _instance._currentLevel != null;
		public static GameLevels Levels => _instance._gameLevels;
		public static Action OnLevelsChanged;

		private void Awake()
		{
			_instance = this;
		}

		private void Start()
		{
			LoadLevel(Levels.LastIndex);
		}

		public static void SaveLevel()
		{
			if (Levels.Exists(_instance._currentLevel.Name))
			{
				var level = Levels[_instance._currentLevel.Name];

				level.Nodes.Clear();

				foreach (var node in _instance._currentNodes)
					level.Nodes.Add(node.Data);
			}
			else
			{
				var level = new LevelData();

				foreach (var node in _instance._currentNodes)
					level.Nodes.Add(node.Data);

				level.Name = _instance._currentLevel.Name;

				Levels.Add(level);
			}
		}

		public static void LoadLevel(int index)
		{
			ClearNodes();

			if (Levels.Count == 0)
				return;

			var level = Levels[index];

			foreach (var nodeData in level.Nodes)
				CreateNode(nodeData);

			_instance._currentLevel = level;
		}

		public static void DeleteLevel()
		{
			ClearNodes();

			Levels.Remove(_instance._currentLevel);

			_instance._currentLevel = null;

			LoadLevel(Levels.LastIndex);

			OnLevelsChanged.Invoke();
		}

		public static void CreateLevel()
		{
			ClearNodes();

			_instance._currentLevel = new LevelData
			{
				Name = $"Level {Levels.Count}"
			};

			Levels.Add(_instance._currentLevel);

			LoadLevel(Levels.LastIndex);

			OnLevelsChanged.Invoke();
		}

		private static void ClearNodes()
		{
			foreach (var node in _instance._currentNodes)
				Destroy(node.View.gameObject);

			_instance._currentNodes.Clear();
		}

		public static void RemoveNode(EditorNode nodeToRemove)
		{
			_instance._currentNodes.Remove(nodeToRemove);
			Destroy(nodeToRemove.View.gameObject);
		}

		public static void CreateNode(NodeData data)
		{
			var editorNodeView = Instantiate(_instance._nodePrefab, _instance._editorLayer.transform);
			editorNodeView.transform.position = data.Position;

			var editorNode = new EditorNode(data, editorNodeView);
			editorNode.View.Init(editorNode);

			_instance._currentNodes.Add(editorNode);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!IsEditable)
				return;

			switch (eventData.button)
			{
				case PointerEventData.InputButton.Right:
					ToolsManager.Open("LevelEdit", eventData.position);
					break;
				case PointerEventData.InputButton.Left:
					ToolsManager.CloseCurrent();
					break;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			ToolsManager.CloseCurrent();
		}
	}
}