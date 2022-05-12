
namespace BioHazard
{
	public class EditorNode
	{
		private NodeType _type;
		private int _startUnits;
		private int _maxUnits;
		private EditorNodeView _view;

		
		public EditorNodeView View => _view;
		
		public EditorNode(NodeData nodeData, EditorNodeView nodeView)
		{
			_type = nodeData.Type;
			_startUnits = nodeData.StartUnits;
			_maxUnits = nodeData.MaxUnits;
			_view = nodeView;
		}

		public NodeData Data => new NodeData
		{
			Type = _type, 
			MaxUnits = _maxUnits, 
			StartUnits = _startUnits, 
			Position = _view.transform.position
		};

		public void ChangeType(NodeType type)
		{
			_type = type;
			_view.SetType(type);
		}

		public void ChangeStartUnits(int startUnits)
		{
			_startUnits = startUnits;
			_view.SetStartUnits(_startUnits);
		}

		public void ChangeMaxUnits(int maxUnits)
		{
			_maxUnits = maxUnits;
		}
	}
}