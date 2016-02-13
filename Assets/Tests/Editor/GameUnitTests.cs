using System;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;


[TestFixture]
public class GameUnitTests
{
	[Test]
	public void EmptyCapabilities()
	{
		var unit = GameUnit.Basic();

		IUnitCapabilities capabilities = unit.Capabilities;

		Assert.AreEqual (0,capabilities.Count);
	}

	[Test]
	public void WalkCapabilitiesNotEmpty()
	{	
		var unit = GameUnit.Basic().CanWalk(10,10);

		IUnitCapabilities capabilities = unit.Capabilities;

		Assert.AreEqual (1,capabilities.Count);
	}

	[Test]
	public void WalkCapabilityVisited()
	{
		var unit = GameUnit.Basic().CanWalk(10,10);

		IUnitCapabilities capabilities = unit.Capabilities;

		var moqVisitor = new Mock<IUnitCapabilityVisitor> ();
	
		capabilities.Visit (moqVisitor.Object);
		moqVisitor.Verify((x) =>  x.WalkCapability(It.IsAny<IWalkCapability>()),Times.Once());
	}
}
