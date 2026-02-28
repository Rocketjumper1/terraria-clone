using Godot;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public partial class CharacterBody2d : CharacterBody2D
{	[Export] public int teleportX = 200;
	[Export] public int teleportY = 200;
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public float MaxSpeed = 2000;
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		if (Input.IsActionJustPressed("teleport_debug"))
		{
			GlobalPosition = new Vector2(teleportX, teleportY);
		}
		
		Vector2 direction = Input.GetVector("left", "right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{	
			
			velocity.X += direction.X * Speed;
			
			if(velocity.X > MaxSpeed)
			{
				velocity.X = MaxSpeed;
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
    public override void _Input(InputEvent @event)
    {
		if (@event.IsActionPressed("leftClick"))
		{
			if(GetParent() is Node2d spawner)
			{
				spawner.spawnBullet(this);
			}
		}
    }
    public override void _Process(double delta)
    {
		if (Input.IsActionPressed("leftClick"))
		{
			if(GetParent() is Node2d spawner)
			{
				spawner.spawnBullet(this);
			}
		}
	}



}
