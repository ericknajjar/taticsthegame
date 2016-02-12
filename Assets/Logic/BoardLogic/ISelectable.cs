using System;


public interface ISelectable
{
	ISelection Select(Point p,IBoardCommandFactory factory);
}


