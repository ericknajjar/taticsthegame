using System;
using System.Collections.Generic;


public interface IBoardCommands
{
	void Visit(IBoardCommandsVisitor vistor);
}

public static class BoardCommands
{
	public static IBoardCommands FromList(IList<IBoardCommand> commands)
	{
		return new BoardCommandsFromList (commands);
	}

	class BoardCommandsFromList: IBoardCommands
	{

		IList<IBoardCommand> m_commands;

		public BoardCommandsFromList (IList<IBoardCommand> commands)
		{
			m_commands = commands;
		}
		
		#region IBoardCommands implementation

		public void Visit (IBoardCommandsVisitor vistor)
		{
			foreach(var command in m_commands)
			{
				command.Visit (vistor);
			
			}
		}

		#endregion


	}
}

