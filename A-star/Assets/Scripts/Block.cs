using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	
	private Vector2 currentPos;

	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject ship;
	[SerializeField]
	private GameObject tradingpost;
	[SerializeField]
	private GameObject star;

	// Use this for initialization
	
	void Start()
	{

		
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	

	public Vector2 GetBlockPosition()
	{
		return currentPos;
	}

	public void SetBlockPosition(float x, float y)
	{
		currentPos.x = x;
		currentPos.y = y;
	}

	private void OnMouseDown()
	{
		Destroy(gameObject);
	}
}
