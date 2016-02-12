using System;

//TODO: Adapter
public class BoardCanWalkPointProviderAdapter: ICanWalkPointProvider
{
	IBoard m_board;
	int m_width;
	int m_height;

	public BoardCanWalkPointProviderAdapter (IBoard board, int width, int height)
	{
		m_board = board;
		m_width = width;
		m_height = height;
	}

	#region IEnumerable implementation

	public System.Collections.Generic.IEnumerator<Point> GetEnumerator ()
	{
		throw new NotImplementedException ();
	}

	#endregion

	#region IEnumerable implementation

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
	{
		throw new NotImplementedException ();
	}

	#endregion
}


