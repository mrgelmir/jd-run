using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HurtBehaviour : ObstacleBehaviour
{

	public override void OnStart (GameObject go)
	{
		go.transform.DOShakeScale (5f, .1f, 5, 90f, false).SetLoops (-1);
		go.transform.DOShakePosition (5f, .1f, 1, 20f, false, false).SetLoops (-1);
	}

	public override void OnHit (Character character)
	{
		character.Hurt ();
	}

}
