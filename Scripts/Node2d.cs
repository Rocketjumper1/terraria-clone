using Godot;
using System;
using System.Xml;


public partial class Node2d : Node2D
{	[Export] PackedScene bulletRef;
	[Export] public int BulletNum = 2;
	[Export] public int spacing = 20;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void spawnBullet(CharacterBody2D player)
	{
		for(int i = -BulletNum; i <= BulletNum; i++)
		{
			Bullet bullet = bulletRef.Instantiate<Bullet>();
			GetTree().Root.AddChild(bullet);
			GD.Print();
			Vector2 player_pos = player.Position;
			player_pos.Y = player_pos.Y + (i * spacing);
			bullet.Position =  player_pos;
			Vector2 player_local_mouse_position = player.ToLocal(GetGlobalMousePosition());
			bullet.direction = player_local_mouse_position.Normalized();
			bullet.Rotation = player_local_mouse_position.Angle() + 3.14159265f / 2;
			bullet.setShooter(player);
		}
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
