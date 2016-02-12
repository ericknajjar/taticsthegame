using System;
using NUnit.Framework;

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
	public void WalkCommandNoyEmpty()
	{
		var unit = GameUnit.Basic().CanWalk();

		ICommands commands = unit.Commands;

		Assert.AreEqual (1,commands.Count);
	}
}
