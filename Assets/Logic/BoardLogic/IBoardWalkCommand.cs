using System;
using System.Collections.Generic;

public interface IBoardWalkCommand: IBoardCommand
{
	IList<Point> PossiblePoints{ get;}
	void Exec(int index);
}


