using System;


public interface IUnitCapabilities
{
	int Count {
		get;
	}

	void Visit(IUnitCapabilityVisitor visitor);
}


