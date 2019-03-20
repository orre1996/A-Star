using System.Collections;
using UnityEngine;


public class Node
{

	private bool walkable;
	[SerializeField]
	public Vector2 worldPosition;
	private int gridX;
	private int gridY;

	public float gCost;
	public float hCost;
	private Node parent;

	int heapIndex;

	public Node(bool _walkable, Vector2 worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public float fCost
	{
		get 
		{
			return gCost + hCost;
		}
		
	}

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}
		set
		{
			heapIndex = value;
		}
	}
	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if(compare == 0)
		{
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}

	public int GridX()
	{
		return gridX;
	}
	public int GridY()
	{
		return gridY;
	}

	public bool Walkable()
	{
		return walkable;
	}


}
