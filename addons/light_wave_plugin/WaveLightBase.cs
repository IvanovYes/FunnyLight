using Godot;
using System;

[Tool]
public partial class WaveLightBase : Node
{
	public override void _EnterTree()
	{

	}
	public virtual DirectionLight RangeAngle{get;}
}

public class DirectionLight{

	public DirectionLight(double angle, double deltaAngle){
		Ange = angle;
		SetSimmetricDeltaAngle(deltaAngle);
	}

	public DirectionLight(double angle, double negDeltaAngle, double posDeltaAngle){
		Ange = angle;
		PosDeltaAngle = posDeltaAngle;
		NegDeltaAngle = negDeltaAngle;
	}
	public double Ange{get;set;}

	private double _posDeltaAngle;
	public double PosDeltaAngle
	{
		get => _posDeltaAngle;
		set{
			if(value < 0)
				throw new ArgumentOutOfRangeException("Delta angle is always more 0");
			_posDeltaAngle = value;
		}
	}

	private double _negDeltaAngle;
	public double NegDeltaAngle
	{
		get => _negDeltaAngle;
		set{
			if(value < 0)
				throw new ArgumentOutOfRangeException("Delta angle is always more 0");
			_negDeltaAngle = value;
		}
	}

	public void SetSimmetricDeltaAngle(double value) {
		PosDeltaAngle = NegDeltaAngle = value;
	}
}

public record class PolarCoords(double Angle, double Distance);

public interface IPolarPrecentStrategy{
	public double GetPolarPrecent(PolarCoords coords, double lambda);
}

public class ConstPolarPrecentStrategy:IPolarPrecentStrategy{
	public ConstPolarPrecentStrategy(double polarPercent){
		_polarPrecent = polarPercent;
	}
	public double GetPolarPrecent(PolarCoords coords, double lambda){
		return _polarPrecent;
	}
	private double _polarPrecent;
}

public interface ICoherentPrecentStrategy{
	public double GetCoherentPrecent(PolarCoords coords, double lambda);
}

public class ConstCoherentPrecentStrategy:ICoherentPrecentStrategy{
	public ConstCoherentPrecentStrategy(double coherentPercent){
		_coherentPercent = coherentPercent;
	}
	public double GetCoherentPrecent(PolarCoords coords, double lambda){
		return _coherentPercent;
	}
	private double _coherentPercent;
}

public record class EMStrenght(double Angle, double Amplitide);

public interface IPolarStrategy{
	public EMStrenght GetPolar(PolarCoords coords, double lambda);
}

public class EllipsePolarAngleStrategy{
	public EllipsePolarAngleStrategy(double XAmplitude, double YAmplitude, double offset){
		_xAmplitude = XAmplitude;
		_yAmplitude = YAmplitude;
		_offset = offset;
	}
	public EMStrenght GetPolar(PolarCoords coords, double lambda, double time){
		var x = _xAmplitude*Math.Cos(2*Math.PI*(coords.Distance + 300000000*time)/lambda);
		var y = _yAmplitude*Math.Cos(2*Math.PI*(coords.Distance + 300000000*time)/lambda + _offset);
		var angle = Math.Atan2(y, x);
		return new EMStrenght(Math.Sqrt(x*x + y*y), angle);
	}
	private double _xAmplitude;
	private double _yAmplitude;
	private double _offset;
	
}

public class LightBuilder{
	
	public double CoherentId{get;set;}

}

public class LightData{
	public double Intensive{get;set;}
	public double Fi{get;set;}
	public double WaveLenght{get;set;}
	public double CoherentId{get;set;}
	public double PolarAngle{get;set;}
	public double PolarPercent{get;set;}
	public double CoherentPercent{get;set;}
}
