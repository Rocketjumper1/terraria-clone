using Godot;
using System;

public partial class ItemSlot : PanelContainer
{
	// Called when the node enters the scene tree for the first time.
	[Export] public TextureRect _rect;
	[Export] public Label _label;
	public void display_item(Item item, int amount)
	{
		if(item != null){
			_rect.Texture = item.texture;
			_rect.Visible = true;
			_label.Text = item.name + " " + (amount > 0 ? amount.ToString() : "");
			return;
		}
		_rect.Texture = null;
		_rect.Visible = false;
		_label.Text = "";

	}
}
