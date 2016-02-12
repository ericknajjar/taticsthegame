using System.Collections;
using NUnit.Framework;
using System;
using Moq;

[TestFixture]
public class BoardTests 
{
	[Test]
	public void EmptySelection()
	{
		var builder = new GameBoardBuilder ();
		var board = builder.GetBoard ();

		var selection = board.Select (Point.Make(1, 1));

		Assert.That (selection.IsEmpty);
	}

	[Test]
	public void NonEmptySelection()
	{
		var builder = new GameBoardBuilder ();

		var moq = new Mock<ISelectable> ();
		var moqSelection = new Mock<ISelection> ();

		moqSelection.SetupGet ((x) => x.IsEmpty).Returns (false);
		moq.Setup ((x) => x.Select ()).Returns (moqSelection.Object);

		builder.AddSelectable (Point.Make(1, 1), moq.Object);
			
		var board = builder.GetBoard();
		var selection = board.Select (Point.Make(1, 1));

		Assert.That(!selection.IsEmpty);
	}

	[Test]
	public void SameSelectionObject()
	{
		var builder = new GameBoardBuilder ();

		var moq = new Mock<ISelectable> ();
		var moqSelection = new Mock<ISelection> ();

		moqSelection.SetupGet ((x) => x.IsEmpty).Returns (false);
		var ret = moqSelection.Object;
		moq.Setup ((x) => x.Select ()).Returns (ret);

		builder.AddSelectable (Point.Make(1, 1), moq.Object);

		var board = builder.GetBoard();
		var selection = board.Select (Point.Make(1, 1));

		Assert.AreEqual(ret,selection);
	}


}
