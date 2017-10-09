using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	private readonly int bJump = Animator.StringToHash("jumping");
	private readonly int bRun = Animator.StringToHash("running");
	private readonly int tHurt = Animator.StringToHash("hurt");

	public System.Action OnCollect;

	[SerializeField]
	private Animator anim;
	[SerializeField]
	private Transform visual;
	[SerializeField]
	private SpriteRenderer sprite;
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

	public SpriteRenderer Sprite
	{ get { return sprite; } }

	public bool Running
	{
		set { anim.SetBool(bRun, value); }
	}

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
			.OnComplete(() =>
	   {
		   jumping = false;
	   });

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

	public void Hurt()
	{
		// Change sprite
		sprite.color = Color.red;
		sprite.DOColor(Color.white, 1.5f);

		anim.SetTrigger(tHurt);
	}

	public void Collect()
	{
		if (OnCollect != null)
		{
			OnCollect();
		}
	}

}
