using System;


public class GameUnit: IGameUnit
{
	static IUnitCapabilities m_emptyCapabilities = new EmptyUnitCapabilities();

	private GameUnit ()
	{
		
	}

	public static IGameUnit Basic()
	{
		return new GameUnit ();
	}

	#region IGameUnity implementation

	public IUnitCapabilities Capabilities {
		get {
			return m_emptyCapabilities;
		}
	}

	#endregion
}

