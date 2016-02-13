using System;
using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class WorldLogicCoodinateTransformTests
{
	const float m_scale = 0.32f;
	WorldLogicCoordinateTransform m_transformer = new WorldLogicCoordinateTransform(m_scale,10,10);

	[Test]
	public void WorldFiveFive()
	{
		var point = m_transformer.WorldToPoint (Vector2.zero);

		Assert.AreEqual (Point.Make (4, 4), point);
	}

	[Test]
	public void WorldFourFive()
	{
		var point = m_transformer.WorldToPoint (new Vector2(-(m_scale+0.01f),0.0f));

		Assert.AreEqual (Point.Make (3, 4), point);
	}

	[Test]
	public void WorldZeroZero()
	{
		float val = -(m_scale * 5) + 0.01f;

		var point = m_transformer.WorldToPoint (new Vector2(val,val));

		Assert.AreEqual (Point.Make (0, 0), point);
	}

}


