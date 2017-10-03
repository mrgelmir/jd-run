using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBehaviour : ObstacleBehaviour
{

	[SerializeField]
	private Sprite collectable;

	public override void OnHit (Character character)
	{
		character.Collect (collectable);
	}
	
}
