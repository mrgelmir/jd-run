using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class JumpBehaviour : ObstacleBehaviour
{
	public override void OnHit(Character character)
	{
		character.Jump();
		
	}
}
