using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	private ObstacleBehaviour behaviour;


	public void OnTriggerEnter2D (Collider2D collision)
	{

		CharacterCollision c = collision.GetComponent<CharacterCollision> ();

		if (c == null)
			return;

		print ("collision");

		if (behaviour != null)
		{
			behaviour.OnHit (c.Character);

			if (behaviour.DestroyOnPickup)
			{
				Destroy (gameObject);
			}

		}
	}
}

public abstract class ObstacleBehaviour : ScriptableObject
{
	[SerializeField]
	private bool destroyOnPickup = true;

	public bool DestroyOnPickup	{ get { return destroyOnPickup; } }

	public abstract void OnHit (Character character);
}