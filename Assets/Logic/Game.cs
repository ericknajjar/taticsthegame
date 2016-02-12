using System;
using u3dExtensions.Engine.Runtime;
using u3dExtensions;
using UnityEngine;
using u3dExtensions.IOC;
using u3dExtensions.IOC.extensions;
using u3dExtensions.Events;

//TODO: Mediator
public class Game: MonoBehaviour
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

	/*	m_boardView.OnCellClicked.Register((x,y) =>{

			var point = Point.Make(x,y);

			if(m_currentSelection.IsEmpty)
			{
				
				m_currentSelection = m_logicBoard.Select(point);
			}
			else
			{
				m_currentSelection.Commands.Visit(new WalkVisitor(point));
				m_currentSelection = m_logicBoard.Select (Point.Make(-1,-1));
			}
		});*/

	}

	void AddGameUnity(GameBoardBuilder builder, Point p)
	{
		var pointProvider = new BoardCanWalkPointProviderAdapter (builder, 10, 10);

		var unit = GameUnit.Basic ().CanWalk (pointProvider);
		//var selectable = new GameUnitySelectableAdapter (unit);

		//m_boardView.AddUnitView (p.X, p.Y);

		//builder.AddSelectable (p,selectable);
	}

	[BindingProvider(DependencyCount = 1)]
	public static Game NewGame(IBindingContext context)
	{
		var boardView = context.Get<BoardView> (InnerBindingNames.Empty,10,10);

		return new Game(boardView);
	}



}


