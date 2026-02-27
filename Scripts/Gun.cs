
using Godot;
[GlobalClass]

public partial class Gun : Item
{
    [Export] int dmg = 20;
    public override void use()
    {
        GD.Print("Gun Item Used");
    }
}