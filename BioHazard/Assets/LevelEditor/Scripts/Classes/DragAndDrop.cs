
using UnityEngine;
using UnityEngine.EventSystems;

namespace BioHazard
{
	public class DragAndDrop : MonoBehaviour, IDragHandler
	{
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (Input.GetMouseButton(0))
			{
				gameObject.transform.position = new Vector2(
					Mathf.Clamp(eventData.position.x, 0, Screen.width),
					Mathf.Clamp(eventData.position.y, 0, Screen.height));
			}
		}
	}
}