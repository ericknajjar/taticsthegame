using System.Collections;
using NUnit.Framework;
using System;


[TestFixture]
public class BoardTests 
{
	[Test]
	public void EmptySelectionCantWalk()
	{
		var board = new Board (10,10);

		board.Select (1, 1);

		Assert.Throws<Exception>(()=>board.Walk (2,2) );
	}

	[Test]
	public void NewPositionNotEmptyAfterUnityWalk()
	{
		var board = new Board (10,10);

		board.AddUnity (1, 1);
		board.Select (1, 1);
		board.Walk (2,2);

		Assert.That(board.HasUnity (2, 2));
	}

	[Test]
	public void UpdatedSelectedAfterWalk()
	{
		var board = new Board (10,10);

		board.AddUnity (1, 1);
		board.Select (1, 1);
		board.Walk (2,2);

		Assert.DoesNotThrow(()=> board.Walk (1,1));
	}

	[Test]
	public void AddUnityMakesUnityExist()
	{
		var board = new Board (10,10);

		board.AddUnity (1, 1);

		Assert.That(board.HasUnity (1, 1));
	}

	[Test]
	public void CantWalkIntoOcupiedCell()
	{
		var board = new Board (10,10);

		board.AddUnity (1, 1);
		board.AddUnity (2, 2);
		board.Select (1, 1);

		Assert.Throws<Exception>(()=> board.Walk (2, 2));
	}

	[Test]
	public void EmptyCellDontHaveUnity()
	{
		var board = new Board (10,10);

		Assert.That(!board.HasUnity (1, 1));
	}

}
