
using System;

namespace BioHazard
{
	public class Node
	{
		public static event Action<Node> Click;

		private NodeType _type;

		private int _maxUnitsCount;

		private int _currentUnitsCount;

		private NodeView _view;

		public int MaxUnitsCount => _maxUnitsCount;
		public int CurrentUnitsCount => _currentUnitsCount;

		public NodeType Type => _type;

		public NodeView View => _view;

		public Node(NodeType type, int startUnitsCount, int maxUnitsCount)
		{
			_type = type;
			_currentUnitsCount = startUnitsCount;
			_maxUnitsCount = maxUnitsCount;

			NodeView.EnterNodeView += OnEnterView;
			NodeView.ClickNodeView += OnClickView;
			NodeManager.AddUnitsCount += OnAddUnit;
		}

		public Node(NodeData nodeData)
		{
			_type = nodeData.Type;
			_currentUnitsCount = nodeData.StartUnits;
			_maxUnitsCount = nodeData.MaxUnits;

			NodeView.EnterNodeView += OnEnterView;
			NodeView.ClickNodeView += OnClickView;
			NodeManager.AddUnitsCount += OnAddUnit;
		}

		private void OnAddUnit()
		{
			if (Type != NodeType.Neutral && _currentUnitsCount < MaxUnitsCount)
			{
				_currentUnitsCount++;
				_view.SetCountUnit(_currentUnitsCount);
			}

			if (Type == NodeType.Neutral)
				_view.SetCountUnit(_currentUnitsCount);
		}

		public void SetCountUnit(int count)
		{
			_currentUnitsCount = count;
			_view.SetCountUnit(_currentUnitsCount);
		} 

		public void SetCurrentUnitsCount(int count)
		{
			_currentUnitsCount = count;
		}

		public void SetType(NodeType nodeType)
		{
			_type = nodeType;
			_view.SetType(nodeType);
			
			_view.Deactivate();
		}

		public void SetNodeView(NodeView nodeView)
		{
			_view = nodeView;
		}

		private void OnEnterView(NodeView nodeView)
		{
			if (nodeView != _view) 
				return;
			
			NodeManager.ActivateNode(this);
		}

		private void OnClickView(NodeView nodeView)
		{
			if(nodeView == _view)
				Click?.Invoke(this);
		}
	}
}