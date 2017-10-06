using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	private ObstacleBehaviour behaviour;
	public UnityEvent onHit;

	protected void Start ()
	{
		if (behaviour != null)
			behaviour.OnStart (gameObject);
	}

	protected void OnTriggerEnter2D (Collider2D collision)
	{
		CharacterCollision c = collision.GetComponent<CharacterCollision> ();

		if (c == null)
			return;

		onHit.Invoke ();

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

	public bool DestroyOnPickup { get { return destroyOnPickup; } }

	public abstract void OnHit (Character character);

	public virtual void OnStart (GameObject go)
	{
	}
}