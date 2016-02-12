using System;

class EmptyUnitCapabilities: IUnitCapabilities
{
	#region ICommands implementation
	public int Count {
		get {
			return 0;
		}
	}
	#endregion

	public void Visit (IUnitCapabilityVisitor visitor)
	{

	}
}
