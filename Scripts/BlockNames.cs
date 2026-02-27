using Godot;
using System;

public partial class BlockNames : Node
{
	public struct block_pair
	{
	public int ID;
	public Vector2I locationInMap;
	}
	// Called when the node enters the scene tree for the first time.
	public static readonly block_pair GRASS = new block_pair {ID = 0, locationInMap = new Vector2I(0, 0)}; 
	public static readonly block_pair ROCK = new block_pair {ID = 1, locationInMap = new Vector2I(0, 0)}; 
}
