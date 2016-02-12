using System;
using System.Collections.Generic;

public interface IBoardCommandFactory
{
	IBoardCommand BuildWalkCommand(Point from,IList<Point> posibilities);
}


