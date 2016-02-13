using System;


public interface ICommandResult
{
	
}

class EmptyResult: ICommandResult
{

}

class AUnitMoved: ICommandResult
{
	public Point From{ get; private set;}
	public Point To{ get; private set;}

	public AUnitMoved (Point from, Point to)
	{
		this.From = from;
		this.To = to;
	}
	
}

