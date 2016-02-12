using System;


public class GameUnit: IGameUnit
{
	static ICommands m_emptyCommands = new EmptyCommands();

	GameUnit ()
	{
		
	}

	public static IGameUnit Basic()
	{
		return new GameUnit ();
	}

	#region IGameUnity implementation

	public ICommands Commands {
		get {
			return m_emptyCommands;
		}
	}

	#endregion
}

