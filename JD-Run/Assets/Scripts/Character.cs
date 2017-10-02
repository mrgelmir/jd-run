using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
	private readonly int bJump = Animator.StringToHash("jumping");

	[SerializeField]
	private Animator anim;
	[SerializeField]
	private Transform visual;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip[] footFallClips;
	[SerializeField]
	private float jumpHeight = 1f;
	[SerializeField]
	private float jumpDuration = 1f;

	public bool Enabled = true;
	private bool jumping = false;

	protected void Start()
	{
		if (anim == null)
			anim = GetComponent<Animator>();
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	public void Jump()
	{
		if (!Enabled || jumping)
			return;

		jumping = true;

		anim.SetBool(bJump, true);
		visual.DOMove(transform.position + (Vector3.up * jumpHeight), jumpDuration / 2f)
			.SetLoops(2, LoopType.Yoyo)
			.OnComplete(() => { jumping = false; });

		StartCoroutine(EndJumpRoutine());
	}

	private IEnumerator EndJumpRoutine()
	{
		yield return new WaitForSeconds(jumpDuration * .75f);
		anim.SetBool(bJump, false);
	}

	public void FootFall()
	{
		audioSource.PlayOneShot(footFallClips[Random.Range(0, footFallClips.Length)]);
	}

	
}
