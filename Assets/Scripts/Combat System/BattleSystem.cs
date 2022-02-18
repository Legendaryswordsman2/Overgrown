using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
	public static BattleSystem instance;

	[Header("Units")]
	[SerializeField, ReadOnlyInspector] GameObject[] enemies;

	[Header("Refernces")]
	[SerializeField] GameObject enemyParent;

	[field: Space]

	[field: SerializeField] public GameObject playerChoices { get; private set; }

	[field: Space]

	[field: Header("Adjustements")]
    [field: SerializeField]	public float backToBlockAnimationDelay { get; private set; } = 1;

	private void Awake()
	{
		instance = this;

		SetupCombat();
	}

	void SetupCombat()
	{

	}

	private void OnValidate()
	{
		if(enemyParent != null)
		{
			
		}
	}
}
