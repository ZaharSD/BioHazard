
using UnityEngine;
using UnityEngine.EventSystems;

namespace BioHazard
{
	public class Background : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			NodeManager.ResetSelectedNodes();
		}
	}
}