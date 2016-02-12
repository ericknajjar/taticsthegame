using System;

//TODO: Adapter
using System.Collections.Generic;


public class BoardCanWalkPointProviderAdapter: ICanWalkPointProvider
{
	GameBoardBuilder m_board;
	int m_width;
	int m_height;

	public BoardCanWalkPointProviderAdapter (GameBoardBuilder board, int width, int height)
	{
		m_board = board;
		m_width = width;
		m_height = height;
	}

	List<Point> GetValidPoints()
	{
		List<Point> validPoints = new List<Point> (m_width * m_height);

		var board = m_board.GetBoard ();

		for(int x=0;x<m_width;++x)
		{
			for(int y=0;y<m_width;++y)
			{
				var point = Point.Make (x,y);

				if (board.IsEmpty (point)) 
				{
					validPoints.Add (point);
				}
			}
		}

		return validPoints;
	}

	#region IEnumerable implementation

	public System.Collections.Generic.IEnumerator<Point> GetEnumerator ()
	{
		return GetValidPoints ().GetEnumerator ();
	}

	#endregion

	#region IEnumerable implementation

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
	{
		return GetValidPoints ().GetEnumerator ();
	}

	#endregion
}


