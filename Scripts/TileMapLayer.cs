using Godot;
using System;
using System.ComponentModel;


public partial class TileMapLayer : Godot.TileMapLayer
{	[Export] int surfaceHeight = 1;
	[Export] int maxHillHeight = 10;
	[Export] float frequency = 0.01f;
	[Export] int depth = 50;
	[Export] int worldWidth = 200;
	[Export] int seed = (int)GD.Randi();
	// Called when the node enters the scene tree for the first time.
	public void setCellBlock(BlockNames.block_pair pair, Vector2I position)
	{
		SetCell(position, pair.ID, pair.locationInMap);
	}
	public override void _Ready()
	{
		FastNoiseLite _noise = new FastNoiseLite();
		_noise.Seed = seed;
		_noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
		_noise.Frequency = 0.01f;
		
		for(int x = -worldWidth; x < worldWidth; x++)
		{
			int height = (int)(_noise.GetNoise1D(x) * maxHillHeight) + surfaceHeight;
			generateLayer(BlockNames.GRASS, BlockNames.ROCK, new Vector2I(x, height));
			
		}
	}
	public void generateLayer(BlockNames.block_pair top, BlockNames.block_pair bottom, Vector2I pos)
	{
		setCellBlock(BlockNames.GRASS, pos);
			for(int i = 1; i < depth; i++)
			{
				Vector2I lowerLayerPos = new Vector2I(pos.X, pos.Y + i);
				setCellBlock(BlockNames.ROCK, lowerLayerPos);
			}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
