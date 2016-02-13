using System;
using u3dExtensions.Engine.Runtime;
using u3dExtensions;
using UnityEngine;
using u3dExtensions.IOC;
using u3dExtensions.IOC.extensions;
using u3dExtensions.Events;

//TODO: Mediator
public class Game
{
	BoardView m_boardView;

	IBoard m_logicBoard;

	ISelection m_currentSelection;

	public Game(BoardView boardView)
	{
		m_boardView = boardView;
		GameBoardBuilder builder = new GameBoardBuilder ();
		AddGameUnity (builder,Point.Make(0,4));

		m_logicBoard = builder.GetBoard ();

		m_currentSelection = m_logicBoard.Select (Point.Make(-1,-1));

		m_boardView.OnCellClicked.Register((x,y) =>{

			//TODO: Usar state pattern
			var point = Point.Make(x,y);
			Debug.Log(point);

			if(m_currentSelection.IsEmpty)
			{
				
				m_currentSelection = m_logicBoard.Select(point);
			}
			else
			{
				var visitor = new WalkVisitor(point);

				m_currentSelection.Commands.Visit(visitor);

				m_boardView.AddResult(visitor.Result);
				m_currentSelection = m_logicBoard.Select (Point.Make(-1,-1));
			}
		});

	}

	void AddGameUnity(GameBoardBuilder builder, Point p)
	{
		var unit = GameUnit.Basic ().CanWalk (10,10);
		var selectable = new GameUnitSelectableAdapter (unit);


		m_boardView.AddUnitView (p);
	
		builder.AddSelectable (p,selectable);
	}

	[BindingProvider(DependencyCount = 1)]
	public static Game NewGame(IBindingContext context)
	{
		WorldLogicCoordinateTransform transformer = new WorldLogicCoordinateTransform(0.32f,10,10);
		var boardView = context.Get<BoardView> (InnerBindingNames.Empty,10,10,transformer);

		return new Game(boardView);
	}

	class WalkVisitor: BoardCommandsVisitorAdapter
	{
		Point m_target;
		public ICommandResult Result{ get; private set;}

		public WalkVisitor(Point target)
		{
			m_target = target;
			Result = new EmptyResult();
		}

		public override void WalkCommand (IBoardWalkCommand command)
		{
			var index = command.PossiblePoints.IndexOf (m_target);

			if(index >= 0)
				Result = command.Exec (index);
		}
	}
		

}


