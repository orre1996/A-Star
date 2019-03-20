using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPost : Config
{

	private Gridgenerator grid;
	

	[SerializeField]
	private GameObject star;
	[SerializeField]
	private GameObject player;

	private Vector2 currentPos;

	bool blocked = false;
	bool cooldown = false;
	private float count = 0;
	private bool NewStarPos = false;
	// Use this for initialization
	private void Awake()
	{
		currentPos.x = Random.Range(-15, 15);
		currentPos.y = Random.Range(-15, 15);
		transform.position = currentPos;
	}
	void Start()
	{
		grid = FindObjectOfType<Gridgenerator>();
	}

	// Update is called once per frame
	void Update()
	{
		if(cooldown == true)
		{
			count += Time.deltaTime;
			if(count > 1)
			{
				count = 0;
				cooldown = false;
			}
		}

		if (grid.BlockTradingpostSamePos(this) == true)
		{

			Replace();
		}

		float minX = transform.position.x - 0.5f;
		float maxX = transform.position.x + 0.5f;
		float minY = transform.position.y - 0.5f;
		float maxY = transform.position.y + 0.5f;

		if (minX < player.transform.position.x && maxX > player.transform.position.x && minY < player.transform.position.y && maxY > player.transform.position.y && cooldown == false)
		{
			NewStarPos = true;
			cooldown = true;
			Invoke("TurnOffNewStarPos", 0.1f);
		}

		blocked = false;
	}

	void TurnOffNewStarPos()
	{
		NewStarPos = false;
	}

	void Replace()
	{
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
			transform.position = currentPos;
	}

	public bool NewStarPosition()
	{
		return NewStarPos;
	}

	public void OnTriggerStay(Collider other)
	{

		if (other.tag == "Block" || other.tag == "Star" || other.tag == "Ship" || other.tag == "Chaser")
		{

			blocked = true;
		}
	}

	public Vector2 GetPostPosition()
	{
		return currentPos;
	}
}
