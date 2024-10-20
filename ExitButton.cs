using Godot;
using System;

public partial class ExitButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ButtonDown += PressButton;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void PressButton()
	{
		GetTree().Quit();	
	}
}