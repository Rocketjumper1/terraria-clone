using Godot;
using System;

public partial class InventoryDisplay : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene invSlot;
	[Export] public int slots = 20;
	[Export] public GridContainer _grid;
	[Export] public Item minishark;

    public override void _Ready()
    {
        CreateLayout();
		CallDeferred(nameof(debugFill));
    }
	private void debugFill()
	{
		displayItemInSlot(0, minishark, 1);
	}
    private void CreateLayout()
    {
		foreach(Node child in _grid.GetChildren())
		{
			child.QueueFree();
		}
		for(int i = 0; i < slots; i++)
		{		ItemSlot slot = invSlot.Instantiate<ItemSlot>();
				_grid.AddChild(slot);
				
		}
    }
	private void displayItemInSlot(int index, Item item, int count)
	{
		if(index >= 0 && index < _grid.GetChildCount())
		{
			var slot =_grid.GetChild(index) as ItemSlot;
			slot.display_item(item, count);
		}
	}

}
