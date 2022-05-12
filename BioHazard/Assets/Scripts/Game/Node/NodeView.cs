
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace BioHazard
{
	public class NodeView : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
	{
		[SerializeField] private Image _nodeBubble;
		[SerializeField] private Image _nodeCore;
		[SerializeField] private Text _currentUnits;
		[SerializeField] private GameObject _circle;
		
		public static Action<NodeView> ClickNodeView;
		public static Action<NodeView> EnterNodeView;

		private Tween _rotationCircle;

		private void Start()
		{
			var rotation = new Vector3(0, 0, 360);

			_rotationCircle = _circle.transform.DOLocalRotate(rotation, 4).SetLoops(-1, LoopType.Incremental)
				.SetEase(Ease.Linear).SetRelative();
			_rotationCircle.Pause();
		}

		public void Init(int startUnits, NodeType type, Vector3 position)
		{
			SetStartUnit(startUnits);

			gameObject.transform.position = position;

			SetType(type);
		}

		public void SetCountUnit(int countUnit)
		{
			if(this!= null)
				_currentUnits.text = countUnit.ToString();
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

		private void SetStartUnit(int startUnits)
		{
			_currentUnits.text = startUnits.ToString();

			transform.localScale = startUnits == 0
				? new Vector3(1.0f, 1.0f, 1.0f)
				: new Vector3(0.05f * startUnits, 0.05f * startUnits, 0.05f * startUnits);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			EnterNodeView?.Invoke(this);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ClickNodeView?.Invoke(this);
		}

		public void Deactivate()
		{
			_rotationCircle.Pause();
			_circle.SetActive(false);
		}

		public void Activate()
		{
			_circle.SetActive(true);
			_rotationCircle.Restart();
		}

		private void OnDisable()
		{
			
		}
	}
}