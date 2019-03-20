using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// https://github.com/SebLague

public class Grid : Config
{

	[SerializeField]
	private LayerMask unwalkableMask;
	[SerializeField]
	private Vector2 gridWorldSize;
	[SerializeField]
	private float nodeRadius;
	private Node[,] grid;

	private float nodeDiamater;
	private int gridSizeX, gridSizeY;

	public void Start()
	{
		nodeDiamater = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);
		CreateGrid();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) || path == null)
		{
			CreateGrid();
		}
	}
	
	public Node[,] TheGrid
	{
		get
		{
			return grid;
		}
	}
	public void CreateGrid()
	{

		grid = new Node[gridSizeX, gridSizeY];
		Vector3 LeftBottom = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.up * (gridWorldSize.y / 2);

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = LeftBottom + Vector3.right * (x * nodeDiamater + nodeRadius) + Vector3.up * (y * nodeDiamater + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public Node NodeWorldPoint(Vector3 worldPosition)
	{
		float NodeX = (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float NodeY = (worldPosition.y - transform.position.y + gridWorldSize.y / 2) / gridWorldSize.y;

		NodeX = Mathf.Clamp01(NodeX);
		NodeY = Mathf.Clamp01(NodeY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * NodeX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * NodeY);

		return grid[x, y];
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}
				int checkX = node.GridX() + x;
				int checkY = node.GridY() + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}
		return neighbours;
	}

	public List<Node> path;

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

		if (path != null)
		{
			foreach (Node n in path)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiamater - 0.8f));
			}
		}
	}
}
