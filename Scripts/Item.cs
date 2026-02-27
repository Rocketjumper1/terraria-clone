using Godot;

[GlobalClass]

public abstract partial class Item : Resource
{
    [Export] public string name {get; set;}
    [Export] public Texture2D texture {get; set;}
    public abstract void use();

}