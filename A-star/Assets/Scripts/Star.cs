	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Config
{

	private TradingPost tradingPost;
	private Gridgenerator grid;
	private Vector2 currentPos;
	[SerializeField]
	private Transform player;

	bool blocked = false;
	bool taken = false;
	// Use this for initialization
	private void Awake()
	{
		tradingPost = FindObjectOfType<TradingPost>();
		grid = FindObjectOfType<Gridgenerator>();
		currentPos.x = Random.Range(-15, 15);
		currentPos.y = Random.Range(-15, 15);
		transform.position = currentPos;
		if(currentPos == grid.GetChaserPosition())
		{
			TakenReplace();
		}
	}

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		float minX = transform.position.x - 0.5f;
		float maxX = transform.position.x + 0.5f;
		float minY = transform.position.y - 0.5f;
		float maxY = transform.position.y + 0.5f;

		if(grid.BlockStarSamePos(this) == true)
		{
			TakenReplace();
		}

		if(minX < player.position.x && maxX > player.position.x && minY < player.position.y && maxY > player.position.y)
		{

			Invoke("Replace", 0.5f);
		}

		if (tradingPost.NewStarPosition() == true)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			taken = false;
		}

		blocked = false;
	}

	void TakenReplace()
	{
		currentPos.x = Random.Range(-15, 15);
		currentPos.y = Random.Range(-15, 15);
		transform.position = currentPos;
	}

	void Replace()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		currentPos.x = Random.Range(-15, 15);
		currentPos.y = Random.Range(-15, 15);
		transform.position = currentPos;
	}
	
	private void OnMouseDrag()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
	}

	private void OnMouseUp()
	{
		if (blocked != true)
		{
			currentPos = grid.ClosestTile(this);
			transform.position = currentPos;
		}
		else
		{
			transform.position = currentPos;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		
		if (other.tag == "Block" || other.tag == "Post" || other.tag == "Ship" || other.tag == "Chaser")
		{	
			blocked = true;
		}
	}

	public void SetStarPosition(float x, float y)
	{
		currentPos.x = x;
		currentPos.y = y;
	}

	public Vector2 GetStarPosition()
	{
		return currentPos;
	}

	public bool StarTaken()
	{
		return taken;
	}
}
