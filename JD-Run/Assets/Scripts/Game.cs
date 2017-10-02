using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField]
	private Character character;
	[SerializeField]
	private Background bg;

	protected void Start()
	{
		bg.Moving = false;
		character.Enabled = false;

		Vector3 startPos = character.transform.position;
		float distance = 10f;
		character.transform.position += Vector3.left * distance;

		character.transform.DOMove(startPos, distance / bg.Speed)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				bg.Moving = true;
				character.Enabled = true;
			});

	}
}
