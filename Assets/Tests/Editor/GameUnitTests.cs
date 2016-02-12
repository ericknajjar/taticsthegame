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

		ICommands commands = unit.Commands;

		Assert.AreEqual (0,commands.Count);
	}

	[Test]
	public void WalkCommandNotEmpty()
	{
		var points = new List<Point> ();
		var moq = new Mock<ICanWalkPointProvider> ();

		moq.Setup ((x) => x.GetEnumerator()).Returns (()=> points.GetEnumerator ());
			
		var unit = GameUnit.Basic().CanWalk(moq.Object);

		ICommands commands = unit.Commands;

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

		ICommands commands = unit.Commands;

		var moqVisitor = new Mock<ICommandsVisitior> ();

		bool availablePointsOk = false;

		moqVisitor.Setup((x) => x.PickSinglePointCommand(It.IsAny<IPickSinglePointCommand>())).Callback<IPickSinglePointCommand>((cmd)=>{
			availablePointsOk = cmd.AvailablePoints.Contains(pointZero);
			
		});

		commands.Visit (moqVisitor.Object);

		Assert.That (availablePointsOk);
	}
}
