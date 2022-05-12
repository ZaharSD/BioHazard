
using System;
using System.Collections;
using UnityEngine;

namespace BioHazard
{
	public class EnemyAI : MonoBehaviour
    {
		public static event Action AttackEnemyNods;
		public static event Action FindEnemyNods;

		private static float _attackSpeed = 1;
		private static float _chanceAttackNodeWithMinCountUnits = 60;
		private static float _chanceAttackYourNodeWithMinCountUnits = 30;

		public static float ChanceAttackNodeWithMinCountUnits => _chanceAttackNodeWithMinCountUnits;	
		public static float ChanceAttackYourNodeWithMinCountUnits => _chanceAttackYourNodeWithMinCountUnits;

		private bool _isActivate = true;

		private void Awake()
		{
			LevelSelectionWindow.ActivateEnemyAI += OnActivateEnemyAI;
			HUD.DeactivateEnemyAI += OnDeactivateEnemyAI;
		}

		private void OnActivateEnemyAI()
		{
			FindEnemyNods?.Invoke();
			StartCoroutine(Attack());
			_isActivate = true;
		}

		private void OnDeactivateEnemyAI()
		{
			_isActivate = false;
			StopCoroutine(Attack());
		}

		private IEnumerator Attack()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(_attackSpeed, _attackSpeed + 2));
				
				if(_isActivate)
					AttackEnemyNods?.Invoke();
			}
		}

		public static void SetAttackSpeed(float speed)
		{
			_attackSpeed = speed;
		}
		
		public static void SetChanceAttackNodeWithMinCountUnits(float chance)
		{
			_chanceAttackNodeWithMinCountUnits = chance;
		}
		
		public static void SetChanceAttackYourNodeWithMinCountUnits(float chance)
		{
			_chanceAttackYourNodeWithMinCountUnits = chance;
		}
	}
}
