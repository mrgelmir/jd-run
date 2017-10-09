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
	[SerializeField]
	private Animator diaperAnimator;

	[SerializeField]
	private int collectCount = 4;

	private const float startDistance = 20f;
	private const float endDistance = 20f;

	private Vector3 startPos;

	protected void Start()
	{
		// Record start position
		startPos = character.transform.position;
		character.transform.position += Vector3.left * startDistance;

		bg.Moving = false;
		//bg.OnClearRoom += OnRoomClear;

		character.OnCollect += OnCollect;
		character.Enabled = false;
		character.Running = false;

		OnCollect();
		OnCollect();
		OnCollect();
		OnCollect();
	}

	public void StartGame()
	{
		MoveCharacterIn();
	}

	private void MoveCharacterIn()
	{
		character.Running = true;
		character.transform.DOMove(startPos, startDistance / bg.Speed)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
	   {
		   bg.Moving = true;
		   character.Enabled = true;
	   });
	}


	private void OnCollect()
	{
		if (--collectCount <= 0)
		{
			bg.AddFinalRoom();
			character.OnCollect -= OnCollect;
		}
	}

	public void EndGame()
	{
		bg.Moving = false;


		character.Enabled = false;
		character.transform.DOMove(character.transform.position + Vector3.right * endDistance, endDistance / bg.Speed)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
		{
			// Stop moving, turn and look at the baby
			character.Sprite.flipX = true;
			character.Running = false;

			Invoke("DropDiapers", 2f);

		});


	}

	private void DropDiapers()
	{
		// Start covered in poo animation
		character.transform.DOScaleY(0f, .5f).SetDelay(.7f).OnStart(() => { character.Hurt(); });
		diaperAnimator.SetTrigger("Drop");
	}
}
