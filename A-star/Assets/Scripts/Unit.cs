using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Unit : MonoBehaviour {

	[SerializeField]
	private GameObject ship;
	[SerializeField]
	private GameObject tradingpost;
	[SerializeField]
	private GameObject star;
	
	private enum _states { star, ship, tradingpost };
	_states unitStates;

	private float count = 0;
	float speed = 3;
	private float fatigueCounter = 0;
	public int waypointCount = 0;

	private bool fatigueBool = false;
	private bool starTaken = false;
	private bool go = false;
	private bool timer = true;

	List<Node> path = new List<Node>();
	Vector3 waypoint = new Vector3();
	List<Vector3> waypoints;

	Pathfinding pathfind;
	Grid grid;
	Gridgenerator gridgen;

	void Start()
	{
		
		pathfind = FindObjectOfType<Pathfinding>();
		grid = FindObjectOfType<Grid>();
		gridgen = FindObjectOfType<Gridgenerator>();
		
		unitStates = _states.star;
		Invoke("FirstPath", 0.5f);

	}

	void FirstPath()
	{
		pathfind.FindPath(transform.position, star.transform.position);
		path = grid.path;
		unitStates = _states.star;
		go = true;
	}

	Vector3[] Paths(List<Node> path)
	{
		List<Vector3> waypoints = new List<Vector3>();
		if (path != null)
		{
			waypoints.Add(path[0].worldPosition);

			for (int i = 0; i < path.Count; i++)
			{
				waypoints.Add(path[i].worldPosition);
			}
		}
			return waypoints.ToArray();
	}

	void Update()
	{
		
		if (go == true)
		{
			Vector3[] WaypointArray = Paths(path);

			if (transform.position == waypoint)
			{
				waypointCount++;
			}

			if (Input.GetMouseButtonUp(0))
			{
				Invoke("Retrace", 0.1f);
			}

			if (fatigueBool == false)
			{
				fatigueCounter += Time.deltaTime * 0.5f;
			}
			if (fatigueCounter >= 10)
			{
				fatigueBool = true;
				unitStates = _states.ship;
				states();
				fatigueCounter = 0;

			}
			if (timer == false)
			{
				count += Time.deltaTime;
			}
			if (count > 1)
			{
				count = 0;
				timer = true;
			}

			float minX = transform.position.x - 0.3f;
			float maxX = transform.position.x + 0.3f;
			float minY = transform.position.y - 0.3f;
			float maxY = transform.position.y + 0.3f;

			if (minX < ship.transform.position.x && maxX > ship.transform.position.x && minY < ship.transform.position.y && maxY > ship.transform.position.y && fatigueBool == true)
			{
				transform.position = ship.transform.position;
				fatigueBool = false;
				if (starTaken == false)
				{
					unitStates = _states.tradingpost;
					states();
				}
				else if (starTaken == true)
				{
					unitStates = _states.star;
					states();
				}
			}

			if (minX < star.transform.position.x && maxX > star.transform.position.x && minY < star.transform.position.y && maxY > star.transform.position.y && timer == true && unitStates == _states.star)
			{
				transform.position = star.transform.position;
				states();
				
				timer = false;
			}
			else if (minX < tradingpost.transform.position.x && maxX > tradingpost.transform.position.x && minY < tradingpost.transform.position.y && maxY > tradingpost.transform.position.y && timer == true && unitStates == _states.tradingpost)
			{
				transform.position = tradingpost.transform.position;
				states();

				timer = false;
			}
			if (path != null)
			{
				waypoint = WaypointArray[waypointCount];
			}
			if (!Input.GetMouseButton(0) && path != null)
			{
				transform.position = Vector3.MoveTowards(transform.position, WaypointArray[waypointCount], speed * Time.deltaTime);
			}
		}
	}
	void Retrace()
	{
		if (unitStates == _states.ship)
		{

			pathfind.FindPath(transform.position, ship.transform.position);
			path = grid.path;
			unitStates = _states.ship;
			transform.position = gridgen.ClosestTile(this);
			waypointCount = 0;
			return;

		}

		if (unitStates == _states.star)
		{
			pathfind.FindPath(transform.position, star.transform.position);
			path = grid.path;
			unitStates = _states.star;
			transform.position = gridgen.ClosestTile(this);
			waypointCount = 0;
			return;

		}

		if (unitStates == _states.tradingpost)
		{
			pathfind.FindPath(transform.position, tradingpost.transform.position);
			path = grid.path;
			unitStates = _states.tradingpost;
			transform.position = gridgen.ClosestTile(this);
			waypointCount = 0;
			return;

		}
	}

	void states()
	{
		if (unitStates == _states.ship)
		{

			pathfind.FindPath(transform.position, ship.transform.position);
			path = grid.path;
			waypointCount = 0;
			return;

		}
		if (unitStates == _states.star)
		{
			
			pathfind.FindPath(transform.position, tradingpost.transform.position);
			path = grid.path;
			unitStates = _states.tradingpost;
			starTaken = true;
			waypointCount = 0;
			return;

		}
		if (unitStates == _states.tradingpost)
		{
			pathfind.FindPath(transform.position, star.transform.position);
			path = grid.path;
			unitStates = _states.star;
			starTaken = false;
			waypointCount = 0;
			return;
		}
	}
}
