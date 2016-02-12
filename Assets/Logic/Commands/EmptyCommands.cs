using System;

class EmptyCommands: ICommands
{
	#region ICommands implementation
	public int Count {
		get {
			return 0;
		}
	}
	#endregion

	public void Visit (ICommandsVisitior visitor)
	{

	}
}
