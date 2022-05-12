
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BioHazard
{
	public class UnitView : MonoBehaviour
	{
		private Image _core;

		private NodeView _targetNode;

		[SerializeField] private float _time;

		private void OnEnable()
		{
			_core = GetComponent<Image>();
		}
		
		public Tween MoveToNode(NodeView targetNode, NodeType typeStartNode, Vector3 position)
		{
			_targetNode = targetNode;

			gameObject.transform.position = position;

			switch (typeStartNode)
			{
				case NodeType.Allied:
					_core.color = Color.green;
					break;
				case NodeType.Enemy:
					_core.color = Color.red;
					break;
				case NodeType.Neutral:
					_core.color = Color.gray;

					break;
			}

			return transform.DOMove(_targetNode.transform.position, Random.Range(_time, _time + 3), true)
				.SetEase(Ease.Linear);
		}

		private void OnDisable()
		{
			gameObject.transform.position = new Vector3(0, 0, 0);
			_core.color = Color.gray;
			_targetNode = null;
		}
	}
}