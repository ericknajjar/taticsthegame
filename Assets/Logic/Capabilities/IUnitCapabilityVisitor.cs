using System;

//TODO: Visitor

public interface IUnitCapabilityVisitor
{
	void WalkCapability(IWalkCapability command);
}


