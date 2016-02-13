using System;


public interface IBoardCommandsVisitor
{
	void WalkCommand(IBoardWalkCommand command);
}

public class BoardCommandsVisitorAdapter: IBoardCommandsVisitor
{
	public virtual void WalkCommand (IBoardWalkCommand command)
	{
		
	}
		
}

