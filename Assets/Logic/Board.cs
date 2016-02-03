using System;
using u3dExtensions.Engine.Runtime;

public class Board
{
	bool[,] m_internalBoard;

	int m_xSelection = -1;
	int m_ySelection;

	public Board(int widht,int height)
	{
		m_internalBoard = new bool[widht, height];
	}

	public void Select(int x, int y)
	{
		m_xSelection = x;
		m_ySelection = y;
	}

	public int Width
	{
		get{ return m_internalBoard.GetLength (0);}
	}

	public int Height
	{
		get{ return m_internalBoard.GetLength (1);}
	}

	public void Walk(int x, int y)
	{
		if(m_xSelection == -1 || !m_internalBoard[m_xSelection,m_ySelection] || m_internalBoard[x,y])
			throw new Exception ();

		m_internalBoard [m_xSelection, m_ySelection] = false;
		m_internalBoard [x, y] = true;
		m_xSelection = x;
		m_ySelection = y;
	}

	public bool HasUnity(int x, int y)
	{
		return m_internalBoard[x,y];
	}

	public void AddUnity(int x, int y)
	{
		m_internalBoard [x, y] = true;
	}

	[BindingProvider]
	public static Board CreateBoard()
	{
		return new Board (10, 10);
	}
}