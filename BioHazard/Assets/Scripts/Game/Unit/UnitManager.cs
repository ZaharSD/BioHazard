
using System;
using DG.Tweening;
using UnityEngine;

namespace BioHazard
{
	public class UnitManager : MonoBehaviour
	{
		[SerializeField] private int _poolCount = 300;
		[SerializeField] private UnitView _prefab;
		[SerializeField] private Transform _container;

		private static Pool<UnitView> _pool;

		public static event Action<Node, NodeType> UnitMoveComplete;

		private void Awake()
		{
			_pool = new Pool<UnitView>(_prefab, _poolCount, _container);
		}

		private void Start()
		{
			HUD.StopGame += DeactivateUnit;
			NodeManager.MoveUnitsToNode += OnMoveUnitsToNode;
		}
		
		private void DeactivateUnit()
		{
			_pool.ClearPool();
			_pool = new Pool<UnitView>(_prefab, _poolCount, _container);
		}

		private static void OnMoveUnitsToNode(Node startNode, int countUnits, Node targetNode, NodeType type)
		{
			for (var i = 0; i < countUnits; i++)
			{
				var unitView = _pool.GetFreeElement();
				var positionStartNode = startNode.View.gameObject.transform.position;
				var vector3 = new Vector3(
					UnityEngine.Random.Range(positionStartNode.x - 40f,
						positionStartNode.x + 40f),
					UnityEngine.Random.Range(positionStartNode.y + 40f, positionStartNode.y - 40f));
				unitView.MoveToNode(targetNode.View, type, vector3).OnComplete(() =>
				{
					_pool.DeactivateElement(unitView);
					UnitMoveComplete?.Invoke(targetNode, startNode.Type);
				});
			}
		}
	}
}