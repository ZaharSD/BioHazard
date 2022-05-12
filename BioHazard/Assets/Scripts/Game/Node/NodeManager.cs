
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BioHazard
{
	public class NodeManager : MonoBehaviour
	{
		[SerializeField] private int _poolCount = 1;
		[SerializeField] private NodeView _prefab;
		[SerializeField] private Transform _container;

		public static event Action AddUnitsCount;
		public static event Action<Node, int, Node, NodeType> MoveUnitsToNode;
		
		private static readonly List<Node> _selectedNodes = new List<Node>();
		private static readonly List<Node> _enemyNode = new List<Node>();

		private Pool<NodeView> _poolNods;
		private readonly List<Node> _nods = new List<Node>();

		private void Awake()
		{
			_poolNods = new Pool<NodeView>(_prefab, _poolCount, _container);
		}

		private void Start()
		{
			LevelSelectionWindow.LoadLevel += CreateNode;
			
			Node.Click += OnNodeClick;
			UnitManager.UnitMoveComplete += OnUnitMoveComplete;
			EnemyAI.AttackEnemyNods += OnAttackEnemyAI;
			EnemyAI.FindEnemyNods += FindAllEnemyNode;

			HUD.StopGame += DeactivateNode;

			StartCoroutine(AddUnit());

			UnitManager.UnitMoveComplete += OnUnitMoveComplete;
		}

		private static IEnumerator AddUnit()
		{
			while (true)
			{
				AddUnitsCount?.Invoke();

				yield return new WaitForSeconds(1);
			}
		}

		public static void ActivateNode(Node node)
		{
			TurnNode(node);
		}

		private void CreateNode(NodeData data)
		{
			var node = new Node(data);
			_nods.Add(node);
			var nodePosition = data.Position;

			CreateNodeView(data.Type, nodePosition, node);
		}

		private void DeactivateNode()
		{
			_poolNods.ClearPool();
			_selectedNodes.Clear();
			_nods.Clear();
			_enemyNode.Clear();
			_poolNods.DeactivateAllElement();
			
			_poolNods = new Pool<NodeView>(_prefab, _poolCount, _container);
		}

		private void ClearNodes()
		{
			foreach (var node in _nods) 
				node.View.Deactivate();

			_nods.Clear();
		}

		private void CreateNodeView(NodeType typeNode, Vector3 position, Node itemNode)
		{
			var nodeView = _poolNods.GetFreeElement();
			var vector3 = position;

			nodeView.Init(itemNode.CurrentUnitsCount, typeNode, vector3);

			itemNode.SetNodeView(nodeView);
		}

		private void OnNodeClick(Node clickedNode)
		{
			if (_selectedNodes.Count <= 0)
				return;

			TransferUnitsToNode(_selectedNodes, clickedNode);
		}

		private void TransferUnitsToNode(List<Node> attackingNodes, Node targetNode)
		{
			var currentCountUnitNode = 0;

			foreach (var selectedNode in attackingNodes)
			{
				if (selectedNode == targetNode)
					continue;

				if (selectedNode.CurrentUnitsCount != 1)
				{
					currentCountUnitNode = selectedNode.CurrentUnitsCount / 2;
					var count = selectedNode.CurrentUnitsCount - currentCountUnitNode + 1;
					selectedNode.SetCurrentUnitsCount(count);
				}
				MoveUnitsToNode?.Invoke(selectedNode, currentCountUnitNode,
					targetNode, selectedNode.Type);
			}
		}

		private void OnUnitMoveComplete(Node node, NodeType startTypeNode)
		{
			if (node.Type != startTypeNode)
			{
				var currentCount = node.CurrentUnitsCount;

				node.SetCurrentUnitsCount(currentCount - 1);

				if (node.CurrentUnitsCount > 0)
					return;

				NodeCapture(node, startTypeNode);

				if (node.CurrentUnitsCount > 0)
					return;

				var count = Mathf.Abs(node.CurrentUnitsCount);

				node.SetCurrentUnitsCount(count);
			}
			else
			{
				if (node.CurrentUnitsCount >= node.MaxUnitsCount)
					return;

				var count = node.CurrentUnitsCount + 1;

				node.SetCountUnit(count);
			}
		}

		private void NodeCapture(Node node, NodeType installedType)
		{
			node.SetType(installedType);

			if (node.Type == NodeType.Enemy)
			{
				_selectedNodes.Remove(node);
				_enemyNode.Add(node);
			}
			else
				_enemyNode.Remove(node);
		}

		public static void ResetSelectedNodes()
		{
			foreach (var item in _selectedNodes)
			{
				item.View.Deactivate();
			}

			_selectedNodes.Clear();
		}

		private static void TurnNode(Node node)
		{
			if (node.Type != NodeType.Allied)
				return;

			if (_selectedNodes.Exists(x => x == node))
				return;

			node.View.Activate();
			_selectedNodes.Add(node);
		}

		private void FindAllEnemyNode()
		{
			foreach (var node in _nods)
			{
				if (node.Type == NodeType.Enemy)
					_enemyNode.Add(node);
			}
		}

		// AI

		private void OnAttackEnemyAI()
		{
			var nodeTarget = FindNodeForAttack();

			var attackingNodes = FindAttackNodes();

			if (nodeTarget != null)
				TransferUnitsToNode(attackingNodes, nodeTarget);
		}

		private Node FindNodeForAttack()
		{
			Node attackedNode = null;

			if (UnityEngine.Random.Range(1, 100) <= EnemyAI.ChanceAttackNodeWithMinCountUnits)
			{
				attackedNode = _nods.OrderBy(node => node.CurrentUnitsCount).FirstOrDefault(node =>
					node.Type != NodeType.Enemy);
			}
			else if (UnityEngine.Random.Range(1, 100) <= EnemyAI.ChanceAttackYourNodeWithMinCountUnits)
			{
				attackedNode = _nods.OrderBy(node => node.CurrentUnitsCount).FirstOrDefault(node =>
					node.Type == NodeType.Enemy);
			}
			else
			{
				var allNotEnemyNods = _nods.Where(node => node.Type != NodeType.Enemy).ToList();

				if (allNotEnemyNods.Count > 0)
					attackedNode = allNotEnemyNods[UnityEngine.Random.Range(0, allNotEnemyNods.Count - 1)];
			}

			return attackedNode;
		}

		private List<Node> FindAttackNodes()
		{
			var attackingNodes = _enemyNode.OrderBy(node => UnityEngine.Random.Range(int.MinValue, int.MaxValue))
				.Take(UnityEngine.Random.Range(1, _enemyNode.Count + 1)).ToList();

			return attackingNodes;
		}
	}
}