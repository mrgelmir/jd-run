using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

	public System.Action OnClearRoom;

	[Header("Scene References")]
	[SerializeField]
	private List<SpriteRenderer> sprites;
	[SerializeField]
	private SpriteRenderer finalSprite;
	[SerializeField]
	private Transform referencePos;

	[Header("Tweakables")]
	public float Speed = 1f;
	public bool Moving = true;

	private Queue<SpriteRenderer> roomQueue = new Queue<SpriteRenderer>();
	private List<SpriteRenderer> rooms = new List<SpriteRenderer>();

	protected void Start()
	{

		// Populate queue
		for (int i = 1; i < sprites.Count; i++)
		{
			roomQueue.Enqueue(sprites[i]);
		}

		// Assume first room is positioned correctly
		rooms.Add(sprites[0]);
		// Add additional rooms
		AddRoom();
		AddRoom();
		
	}

	protected void Update()
	{
		if (!Moving)
			return;

		// Reverse for in-loop removal
		for (int i = rooms.Count - 1; i >= 0; --i)
		{
			SpriteRenderer sprite = rooms[i];
			sprite.transform.Translate(Vector3.left * Speed * Time.deltaTime);

			if ((sprite.transform.position.x + sprite.bounds.size.x * 1.5f) < referencePos.position.x)
			{
				// Notify listeners
				if (OnClearRoom != null)
					OnClearRoom();

				// Add back to queue
				roomQueue.Enqueue(sprite);

				// Remove from active rooms
				rooms.RemoveAt(i);
				
				// Add new Room
				AddRoom();
			}
		}
	}

	public void AddRoom()
	{
		AddSprite(roomQueue.Dequeue());
	}

	public void AddFinalRoom()
	{
		AddSprite(finalSprite);
	}

	private void AddSprite(SpriteRenderer sprite)
	{
		// Add new sprite and postion relative to previous
		rooms.Add(sprite);
		int i = rooms.Count - 1;
		rooms[i].transform.position = rooms[i - 1].transform.position + Vector3.right * rooms[i - 1].bounds.size.x;
	}

}
