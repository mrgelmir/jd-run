using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehaviour : ObstacleBehaviour
{

	public override void OnHit (Character character)
	{
		character.Hurt ();
	}

}
