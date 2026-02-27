using Godot;
using System;


public partial class Node2d : Node2D
{	[Export] PackedScene bulletRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void spawnBullet(CharacterBody2D player)
	{
		Bullet bullet = bulletRef.Instantiate<Bullet>();
		GetTree().Root.AddChild(bullet);
		GD.Print();
		bullet.Position = player.Position;
		Vector2 player_local_mouse_position = player.ToLocal(GetGlobalMousePosition());
		bullet.direction = player_local_mouse_position.Normalized();
		bullet.Rotation = player_local_mouse_position.Angle() + 3.14159265f / 2;
		bullet.setShooter(player);
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
