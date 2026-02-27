using Godot;
using System;
using System.Numerics;
using System.Threading.Tasks;

public partial class Bullet : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Godot.Vector2 direction = Godot.Vector2.Right;
	[Export] int speed = 200;
	[Export] int AutoDeleteTimeSeconds = 10;
	[Export] int bulletHitPixelRange = 0;
	public Godot.Vector2I speed_vec;

	CollisionObject2D shooter;
	public override void _Ready()
	{
	speed_vec = new Godot.Vector2I(speed, speed);
	 GetTree().CreateTimer(2.0).Timeout += QueueFree;
	}
	public void setShooter(Node2D shooter_node)
	{
		shooter = shooter_node as CollisionObject2D;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void hitHandler(Node2D body, Godot.Vector2 pos)
	{	
		if(body != shooter){
			GD.Print("Body is NOT player");
			if (body is TileMapLayer tileMapLayer)
			{
				GD.Print("i exist");
				for(int i = 0; i <= bulletHitPixelRange; i++)
				{
					Godot.Vector2 hitPoint = pos + (direction * i);
					Godot.Vector2I tilePos = tileMapLayer.LocalToMap(tileMapLayer.ToLocal(hitPoint));

					Godot.Vector2I atlasCoords = tileMapLayer.GetCellAtlasCoords(tilePos);
					
					int sourceId = tileMapLayer.GetCellSourceId(tilePos);
					if(sourceId != -1){
						if(sourceId == BlockNames.GRASS.ID && atlasCoords == BlockNames.GRASS.locationInMap)
						{
							GD.Print("YAY bullet hit grass block");
						}
						tileMapLayer.SetCell(tilePos, -1);
						QueueFree();
					}
				}
			}
			else
			{
				body.QueueFree();
				QueueFree();
			}
		}
		else {GD.Print("Body IS player");}
	}
    public override void _PhysicsProcess(double delta)
    {
        Godot.Vector2 movement = speed_vec * direction *  new Godot.Vector2((float)delta, (float)delta) ;
		Godot.Vector2 nextPos = GlobalPosition + movement;

		var spaceState = GetWorld2D().DirectSpaceState;
		var query = PhysicsRayQueryParameters2D.Create(GlobalPosition, nextPos);

		query.Exclude = new Godot.Collections.Array<Rid>{shooter.GetRid()};

		var resultOfRaycast = spaceState.IntersectRay(query);
		if(resultOfRaycast.Count > 0)
		{
			Node2D hitBody = (Node2D)resultOfRaycast["collider"];
			Godot.Vector2 hitPosition = (Godot.Vector2)resultOfRaycast["position"];
			hitHandler(hitBody, hitPosition);
		}
		else
		{
			Position = nextPos;
		}


    }
	
}
