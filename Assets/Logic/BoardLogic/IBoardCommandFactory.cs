using System;
using System.Collections.Generic;

public interface IBoardCommandFactory
{
	IBoardCommand BuildWalkCommand(Point from,int width, int height);
}


