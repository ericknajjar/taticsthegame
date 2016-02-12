using System;
using NUnit.Framework;

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
		var canWakPointsAdapter = new BoardCanWalkPointProviderAdapter (builder, 10, 10);

		var unity = GameUnit.Basic ().CanWalk (canWakPointsAdapter);
		var zeroPoint = Point.Make (0, 0);

		var selectable = new GameUnitSelectableAdapter (unity);
		builder.AddSelectable (zeroPoint, selectable);

		var board = builder.GetBoard ();

		var selection = board.Select (zeroPoint);

		var visitor = new WalkVisitor (Point.Make (1, 1));

		selection.Commands.Visit (visitor);

		Assert.That (board.IsEmpty (zeroPoint));
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


