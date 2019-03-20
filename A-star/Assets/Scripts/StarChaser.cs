using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarChaser : Config
{

	private Gridgenerator grid;

	private Vector2 currentPos;

	bool blocked = false;
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
		if(grid.BlockChaserSamePos(this) == true)
		{
			Replace();
		}
		
		blocked = false;
	}

	private void OnMouseDrag()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
	}

	void Replace()
	{
		currentPos.x = Random.Range(-15, 15);
		currentPos.y = Random.Range(-15, 15);
		transform.position = currentPos;
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

		if (other.tag == "Block" || other.tag == "Star" || other.tag == "Post" || other.tag == "Ship")
		{
			blocked = true;
		}
	}

	public Vector2 GetChaserPosition()
	{
		return currentPos;
	}
}
