using System;
using NUnit.Framework;
using Moq;

[TestFixture]
public class BoardGameUnitIntegrationTests
{
	public BoardGameUnitIntegrationTests ()
	{
	}

	[Test]
	public void WalkAUnit()
	{
		GameBoardBuilder builder = new GameBoardBuilder ();

		var unity = GameUnit.Basic ().CanWalk (10,10);
		var zeroPoint = Point.Make (0, 0);

		var selectable = new GameUnitSelectableAdapter (unity);
		builder.AddSelectable (zeroPoint, selectable);

		var board = builder.GetBoard ();

		var selection = board.Select (zeroPoint);

		var visitor = new WalkVisitor (Point.Make (1, 1));

		selection.Commands.Visit (visitor);

		Assert.That (board.IsEmpty (zeroPoint));
	}
	[Test]
	public void WalkCmdDontHaveOrigin()
	{
		GameBoardBuilder builder = new GameBoardBuilder ();

		var unity = GameUnit.Basic ().CanWalk (10,10);
		var zeroPoint = Point.Make (0, 0);

		var selectable = new GameUnitSelectableAdapter (unity);
		builder.AddSelectable (zeroPoint, selectable);

		var board = builder.GetBoard ();

		var selection = board.Select (zeroPoint);

		var moq = new Mock<IBoardCommandsVisitor> ();

		moq.Setup((x) => x.WalkCommand(It.IsAny<IBoardWalkCommand>())).Callback<IBoardWalkCommand>((cmd)=>{

			Assert.That(!cmd.PossiblePoints.Contains(zeroPoint));
		});

		selection.Commands.Visit (moq.Object);
	}

	class WalkVisitor: BoardCommandsVisitorAdapter
	{
		Point m_target;

		public WalkVisitor(Point target)
		{
			m_target = target;
		}

		public override void WalkCommand (IBoardWalkCommand command)
		{
			command.Exec (command.PossiblePoints.IndexOf(m_target));
		}
	}
}


