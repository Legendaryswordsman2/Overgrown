using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
	protected override void Setup()
	{
		Debug.Log("Setup");
	}

	public override void ChooseAction()
	{
		throw new System.NotImplementedException();
	}
}
