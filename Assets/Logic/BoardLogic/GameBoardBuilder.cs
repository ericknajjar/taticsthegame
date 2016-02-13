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

		public IBoardCommand BuildWalkCommand (Point from, int width, int height)
		{
			return new WalkCommand (from, width,height,this);
		}

		#endregion

		class WalkCommand: IBoardWalkCommand
		{
			Point m_from;
			IList<Point> m_posibilities;
			Board m_parent;

			public WalkCommand(Point from,int width, int height,Board parent)
			{
				m_from = from;
				m_parent = parent;

				m_posibilities = CalculatePosibilities(width,height);
			}

			List<Point> CalculatePosibilities(int w,int h)
			{
				List<Point> validPoints = new List<Point> (w * h);

				for(int x=0;x<w;++x)
				{
					for(int y=0;y<h;++y)
					{
						var point = Point.Make (x,y);

						if (m_parent.IsEmpty (point)) 
						{
							validPoints.Add (point);
						}
					}
				}

				return validPoints;
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

			public ICommandResult Exec(int index)
			{
				var point = m_posibilities [index];

				ISelectable selectable = null;

				if (m_parent.m_selectables.TryGetValue (m_from, out selectable)) 
				{
					m_parent.m_selectables.Remove (m_from);
					m_parent.m_selectables.Add (point, selectable);
					return new AUnitMoved (m_from, point);
				}

				return new EmptyResult ();
			}

		}
	}
}


