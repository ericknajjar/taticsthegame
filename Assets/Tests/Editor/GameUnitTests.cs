using System;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;


[TestFixture]
public class GameUnitTests
{
	[Test]
	public void EmptyCommands()
	{
		var unit = GameUnit.Basic();

		IUnitCapabilities commands = unit.Capabilities;

		Assert.AreEqual (0,commands.Count);
	}

	[Test]
	public void WalkCommandNotEmpty()
	{
		var points = new List<Point> ();
		var moq = new Mock<ICanWalkPointProvider> ();

		moq.Setup ((x) => x.GetEnumerator()).Returns (()=> points.GetEnumerator ());
			
		var unit = GameUnit.Basic().CanWalk(moq.Object);

		IUnitCapabilities commands = unit.Capabilities;

		Assert.AreEqual (1,commands.Count);
	}

	[Test]
	public void WalkCommandWithDestinations()
	{
		var points = new List<Point> ();
		var pointZero = Point.Make (0, 0);
		points.Add (pointZero);

		var moq = new Mock<ICanWalkPointProvider> ();

		moq.Setup ((x) => x.GetEnumerator()).Returns (()=> points.GetEnumerator ());

		var unit = GameUnit.Basic().CanWalk(moq.Object);

		IUnitCapabilities commands = unit.Capabilities;

		var moqVisitor = new Mock<IUnitCapabilityVisitor> ();

	
		commands.Visit (moqVisitor.Object);
		moqVisitor.Verify((x) =>  x.WalkCapability(It.IsAny<IWalkCapability>()),Times.Once());

	}
}
