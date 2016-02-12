using System;


public interface ICommands
{
	int Count {
		get;
	}

	void Visit(ICommandsVisitior visitor);
}


