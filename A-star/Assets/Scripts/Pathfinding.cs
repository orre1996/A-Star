using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : Config
{
	Grid grid;

	private void Start()
	{
		grid = GetComponent<Grid>();
	}


	void Update()
	{
	
	}

	public void FindPath(Vector3 startPos, Vector3 targetPos)
	{

		grid.path = null;
		Node startNode = grid.NodeWorldPoint(startPos);
		Node targetNode = grid.NodeWorldPoint(targetPos);

		Dictionary<Node, Node> cameFromNode = new Dictionary<Node, Node>();
		List<Node> openSet = new List<Node>();
		List<Node> closedSet = new List<Node>();

		foreach (Node n in grid.TheGrid)
		{
			
			n.hCost = Mathf.Infinity;
			n.gCost = Mathf.Infinity;
			cameFromNode[n] = null;
		}

		openSet.Add(startNode);
		startNode.gCost = 0;
		startNode.hCost = GetDistance(startNode, targetNode);

		while(openSet.Count > 0)
		{

			Node currentNode = openSet[0];
			for(int i = 1; i < openSet.Count; i++)
			{
				if(openSet[i].fCost < currentNode.fCost )
				{
					currentNode = openSet[i];
				}
			}
			if(currentNode == targetNode)
			{

				break;
			}
			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			List<Node> neighbours = grid.GetNeighbours(currentNode);

			foreach (Node n in neighbours)
			{
				if (!n.Walkable() || closedSet.Contains(n))
				{
					continue;
				}
				if (!openSet.Contains(n))
				{
					
					openSet.Add(n);
				}

				float newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, n);
				if(newCostToNeighbour < n.gCost)
				{
				
					n.gCost = newCostToNeighbour;
					n.hCost = GetDistance(n, targetNode);
					cameFromNode[n] = currentNode;
				}
			}
		}
		if (cameFromNode[targetNode] == null)
		{
			return;
		}
		List<Node> path = new List<Node>();
		Node tempNode = targetNode;
		

		while (tempNode != null)
		{
			
			path.Add(tempNode);
			tempNode = cameFromNode[tempNode];
		}

		path.Reverse();
		grid.path = path;

	}

	int GetDistance(Node nodeA, Node nodeB)
		{
			int dstX = Mathf.Abs(nodeA.GridX() - nodeB.GridX());
			int dstY = Mathf.Abs(nodeA.GridY() - nodeB.GridY());

			if (dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);
			else

				return 14 * dstX + 10 * (dstY - dstX);
		}
	}


	

