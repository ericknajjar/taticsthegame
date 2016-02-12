using System;
using u3dExtensions.Engine.Runtime;
using System.Collections.Generic;

class EmptySelection: ISelection
{
	#region ISelection implementation
	public bool IsEmpty {
		get {
			return true;
		}
	}
		

	public IBoardCommands Commands {
		get {
			return BoardCommands.FromList(new List<IBoardCommand> ());
		}
	}
	#endregion

}

//TODO: Builder 
public class GameBoardBuilder
{
	Board m_board;

	public GameBoardBuilder ()
	{
		m_board = new Board ();
	}

	public IBoard GetBoard()
	{
		return m_board;
	}

	public void AddSelectable(Point p,ISelectable selectable)
	{
		m_board.AddSelectable (p,selectable);
	}

	class Board: IBoard, IBoardCommandFactory
	{
		Dictionary<Point,ISelectable> m_selectables = new Dictionary<Point, ISelectable>();

		public ISelection Select(Point p)
		{
			ISelectable selectable = null;

			if (m_selectables.TryGetValue (p, out selectable)) 
			{
				return selectable.Select (p,this);
			}

			return new EmptySelection ();
		}

		public void AddSelectable(Point p,ISelectable selectable)
		{
			m_selectables.Add (p, selectable);
		}

		public bool IsEmpty (Point p)
		{
			return !m_selectables.ContainsKey (p);
		}

		#region IBoardCommandFactory implementation

		public IBoardCommand BuildWalkCommand (Point from, IList<Point> posibilities)
		{
			return new WalkCommand (from,posibilities, this);
		}

		#endregion

		class WalkCommand: IBoardWalkCommand
		{
			Point m_from;
			IList<Point> m_posibilities;
			Board m_parent;

			public WalkCommand(Point from, IList<Point> posibilities, Board parent)
			{
				m_posibilities = posibilities;
				m_from = from;
				m_parent = parent;
			}

			#region IBoardCommand implementation

			public void Visit (IBoardCommandsVisitor vistor)
			{
				vistor.WalkCommand (this);
			}

			public IList<Point> PossiblePoints {
				get {
					return new List<Point> (m_posibilities);
				}	
			}
			#endregion


			public void Exec(int index)
			{
				var point = m_posibilities [index];

				ISelectable selectable = null;

				if (m_parent.m_selectables.TryGetValue (m_from, out selectable)) 
				{
					m_parent.m_selectables.Remove (m_from);
					m_parent.m_selectables.Add (point, selectable);
				}

			}

		}
	}
}


