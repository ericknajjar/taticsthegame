using System;


public interface IBoardCommandsVisitor
{
	void WalkCommand(IBoardWalkCommand command);
}

public class BoardCommandsVisitorAdapter: IBoardCommandsVisitor
{
	#region IBoardCommandVisitor implementation

	public virtual void WalkCommand (IBoardWalkCommand command)
	{
		
	}

	#endregion

}

