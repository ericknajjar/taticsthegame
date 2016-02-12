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
	public void EmptyPoint()
	{
		var builder = new GameBoardBuilder ();
		var board = builder.GetBoard ();

		Assert.That (board.IsEmpty(Point.Make(1, 1)));
	}

	[Test]
	public void NonEmptyPoint()
	{
		var builder = new GameBoardBuilder ();
		var board = builder.GetBoard ();

		var moq = new Mock<ISelectable> ();
		builder.AddSelectable (Point.Make(1, 1), moq.Object);

		Assert.That (!board.IsEmpty(Point.Make(1, 1)));
	}

	[Test]
	public void NonEmptySelection()
	{
		var builder = new GameBoardBuilder ();

		var moq = new Mock<ISelectable> ();
		var moqSelection = new Mock<ISelection> ();

		moqSelection.SetupGet ((x) => x.IsEmpty).Returns (false);

		moq.Setup ((x) => x.Select (It.IsAny<Point>(), It.IsAny<IBoardCommandFactory>())).Returns (moqSelection.Object);

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
		moq.Setup ((x) => x.Select (It.IsAny<Point>(), It.IsAny<IBoardCommandFactory>())).Returns (ret);

		builder.AddSelectable (Point.Make(1, 1), moq.Object);

		var board = builder.GetBoard();
		var selection = board.Select (Point.Make(1, 1));

		Assert.AreEqual(ret,selection);
	}


}
