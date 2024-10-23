using Godot;
using System;

[Tool]
public partial class WaveLightField : Node
{
	public override void _EnterTree()
	{
		foreach(var item in this.GetChildren())
		  if(item is WaveLightBase light){
            
          }
	}
	//public virtual DirectionLight RangeAngle{get;}
}
