using System;


public interface IBoard
{
	ISelection Select(Point p);
	bool IsEmpty(Point p);
}


