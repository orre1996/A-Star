using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridgenerator : Config
{


	[SerializeField]
	private Vector2 mapsize;

	[Header("Tiles")]
	[SerializeField]
	private Tile grid;
	[SerializeField]
	private List<Tile> tiles = new List<Tile>();
	[SerializeField]
	private GameObject TileParent;

	[Header("Blocks")]
	[SerializeField]
	private Block block;
	[SerializeField]
	private List<Block> blocks = new List<Block>();
	[SerializeField]
	private GameObject BlockParent;

	[Header("SpaceShip")]
	[SerializeField]
	private Spaceship ship;

	[Header("Star")]
	[SerializeField]
	private Star star;

	[Header("TradingPost")]
	[SerializeField]
	private TradingPost tradingPost;

	[Header("StarChaser")]
	[SerializeField]
	private StarChaser chaser;
	

	private float closestTarget;
	private Vector2 closestTar;

	

	// Use this for initialization
	void Awake()
	{
		CreateGrid();
	}

	// Update is called once per frame
	void Update()
	{
		RemoveBlocks();
	}

	void CreateGrid()
	{
		for (int x = -15; x < mapsize.x - 15; x++)
		{
			for (int y = -15; y < mapsize.y - 15; y++)
			{
				Tile newTile = Instantiate<Tile>(grid, new Vector3(x, y, +1), Quaternion.identity);
				newTile.name = "X=" + x + "Y=" + y;
				newTile.SetTilePosition(x, y);
				newTile.transform.parent = TileParent.transform;
				tiles.Add(newTile);

				if (Random.value < 0.15f)
				{
					Block newBlock = Instantiate<Block>(block, new Vector3(x, y, -1), Quaternion.identity);
					newBlock.name = "X=" + x + "Y=" + y;
					newBlock.SetBlockPosition(x, y);
					newBlock.transform.parent = BlockParent.transform;
					blocks.Add(newBlock);

				}
			}
		}
	}

	public void AddNewBlock(float x, float y)
	{
		Block newBlock = Instantiate<Block>(block, new Vector3(x, y), Quaternion.identity);
		newBlock.name = "X=" + x + "Y=" + y;
		newBlock.SetBlockPosition(x, y);
		newBlock.transform.parent = BlockParent.transform;
		blocks.Add(newBlock);
	}

	public void RemoveBlocks()
	{
		for (int i = blocks.Count - 1; i > -1; i--)
		{
			if (blocks[i] == null)
				blocks.RemoveAt(i);
		}
	}
	public Vector2 ClosestTile(Spaceship space)
	{
		closestTarget = Mathf.Infinity;
		for (int i = 0; i < tiles.Count; i++)
		{
			float distance = Vector2.Distance(space.transform.position, tiles[i].GetTilePosition());

			if (distance < closestTarget)
			{
				closestTarget = distance;
				closestTar = tiles[i].GetTilePosition();
			}
		}
		return closestTar;
	}
	public Vector2 ClosestTile(StarChaser chaser)
	{
		closestTarget = Mathf.Infinity;
		for (int i = 0; i < tiles.Count; i++)
		{
			float distance = Vector2.Distance(chaser.transform.position, tiles[i].GetTilePosition());

			if (distance < closestTarget)
			{
				closestTarget = distance;
				closestTar = tiles[i].GetTilePosition();
			}
		}
		return closestTar;
	}

	public Vector2 ClosestTile(Unit unit)
	{
		closestTarget = Mathf.Infinity;
		for (int i = 0; i < tiles.Count; i++)
		{
			float distance = Vector2.Distance(unit.transform.position, tiles[i].transform.position);

			if (distance < closestTarget)
			{
				closestTarget = distance;
				closestTar = tiles[i].transform.position;
			}
		}
		return closestTar;
	}

	public Vector2 ClosestTile(Star star)
	{
		closestTarget = Mathf.Infinity;
		for (int i = 0; i < tiles.Count; i++)
		{
			float distance = Vector2.Distance(star.transform.position, tiles[i].GetTilePosition());

			if (distance < closestTarget)
			{
				closestTarget = distance;
				closestTar = tiles[i].GetTilePosition();
			}
		}
		return closestTar;
	}

	public Vector2 ClosestTile(TradingPost post)
	{
		closestTarget = Mathf.Infinity;
		for (int i = 0; i < tiles.Count; i++)
		{
			float distance = Vector2.Distance(post.transform.position, tiles[i].GetTilePosition());

			if (distance < closestTarget)
			{
				closestTarget = distance;
				closestTar = tiles[i].GetTilePosition();
			}
		}
		return closestTar;
	}

	public bool BlockStarSamePos(Star star)
	{
		if (blocks.Count > 0)
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (star.GetStarPosition() == blocks[i].GetBlockPosition())
				{
					return true;
				}
			}
		}
		
			return false;
	}

	public bool BlockShipSamePos(Spaceship ship)
	{
		if (blocks.Count > 0)
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (ship.GetShipPosition() == blocks[i].GetBlockPosition())
				{
					return true;
				}

			}
		}

		return false;
	}


	public bool BlockChaserSamePos(StarChaser chaser)
	{
		if (blocks.Count > 0)
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (chaser.GetChaserPosition() == blocks[i].GetBlockPosition())
				{
					return true;
				}

			}
		}

		return false;
	}

	public bool BlockTradingpostSamePos(TradingPost tp)
	{
		if (blocks.Count > 0)
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (tp.GetPostPosition() == blocks[i].GetBlockPosition())
				{
					return true;
				}

			}
		}

		return false;
	}

	public Vector2 GetShipPosition()
	{
		return ship.GetShipPosition();
	}

	public Vector2 GetStarPosition()
	{
		return star.GetStarPosition();
	}

	public Vector2 GetPostPosition()
	{
		return tradingPost.GetPostPosition();
	}

	public Vector2 GetChaserPosition()
	{
		return chaser.GetChaserPosition();
	}
}




