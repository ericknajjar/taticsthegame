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

	public class Board: IBoard
	{
		Dictionary<Point,ISelectable> m_selectables = new Dictionary<Point, ISelectable>();

		public ISelection Select(Point p)
		{
			ISelectable selectable = null;

			if (m_selectables.TryGetValue (p, out selectable)) 
			{
				return selectable.Select ();
			}

			return new EmptySelection ();
		}

		public void AddSelectable(Point p,ISelectable selectable)
		{
			m_selectables.Add (p, selectable);
		}
	}
}


