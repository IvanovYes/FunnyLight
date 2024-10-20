using Godot;
using System;

[Tool]
public partial class MyButton : Button
{
	public override void _EnterTree()
	{
		Connect("pressed", new Callable(this, MethodName.clicked));
	}

	public void clicked()
	{
		GD.Print("You clicked me!");
	}
}
