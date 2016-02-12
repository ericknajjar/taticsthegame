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

		m_boardView.OnCellClicked.Register((x,y) =>{

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
		});

	}

	void AddGameUnity(GameBoardBuilder builder, Point p)
	{
		var pointProvider = new BoardCanWalkPointProviderAdapter (builder, 10, 10);

		var unit = GameUnit.Basic ().CanWalk (pointProvider);
		var selectable = new GameUnitySelectableAdapter (unit);

		builder.AddSelectable (p,selectable);
	}

	[BindingProvider(DependencyCount = 1)]
	public static Game NewGame(IBindingContext context)
	{
		var boardView = context.Get<BoardView> (InnerBindingNames.Empty,10,10);

		return new Game(boardView);
	}

	class WalkVisitor: ICommandsVisitior
	{
		Point m_target;

		public WalkVisitor(Point target)
		{
			m_target = target;
		}

		#region ICommandsVisitior implementation

		public void PickSinglePointCommand (IPickSinglePointCommand command)
		{
			int index = command.AvailablePoints.IndexOf (m_target);

			if(index>=0)
				command.Pick (index);
		}

		#endregion
	}

	//TODO: Adapter
	class GameUnitySelectableAdapter: ISelectable
	{
		IGameUnit m_unit;
		public GameUnitySelectableAdapter(IGameUnit unit)
		{
			m_unit = unit;
		}

		#region ISelectable implementation

		public ISelection Select ()
		{
			return new GameUnitySelection (m_unit);
		}

		#endregion

		class GameUnitySelection: ISelection
		{
			IGameUnit m_unit;
			public GameUnitySelection(IGameUnit unit)
			{
				m_unit = unit;
			}

			#region ISelection implementation

			public bool IsEmpty {
				get {
					return false;
				}
			}

			public ICommands Commands {
				get {
					return m_unit.Commands;
				}
			}
			#endregion
		}
	}
}


