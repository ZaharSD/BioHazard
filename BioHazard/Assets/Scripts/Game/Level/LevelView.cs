
using BioHazard;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
	[SerializeField] private Text _levelNumber;
	
	
	public void SetState(LevelState state)
	{
		switch (state)
		{
			case LevelState.Unlocked:
				break;
			case LevelState.Locked:
				break;
		}
	}
}