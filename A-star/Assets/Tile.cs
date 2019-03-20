using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
	private Vector2 currentPos;

	private Gridgenerator grid;
	// Use this for initialization
	void Start()
	{
		grid = FindObjectOfType<Gridgenerator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			grid.AddNewBlock(currentPos.x, currentPos.y);
		}
	}


	private void OnMouseDown()
	{
		if (grid.GetShipPosition() != currentPos || currentPos != grid.GetStarPosition() || currentPos != grid.GetPostPosition())
		{
			grid.AddNewBlock(currentPos.x, currentPos.y);
		}
	}


	public void SetTilePosition(float x, float y)
	{
		currentPos.x = x;
		currentPos.y = y;
	}

	public Vector2 GetTilePosition()
	{
		return currentPos;
	}
}
