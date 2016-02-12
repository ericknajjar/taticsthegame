using System;
using System.Collections.Generic;


public interface IPickSinglePointCommand
{
	void Pick (int pointIndex);
	IList<Point> AvailablePoints{ get;}
}


