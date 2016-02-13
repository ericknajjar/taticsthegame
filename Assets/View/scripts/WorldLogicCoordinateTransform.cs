using System;
using UnityEngine;

public class WorldLogicCoordinateTransform
{
	Vector2 m_offset;
	float m_scale;

	public WorldLogicCoordinateTransform (float scale, int width, int height)
	{
		m_offset = new Vector2 (width / 2.0f, height / 2.0f)*scale;			
		m_scale = scale;
	}
		
	public Point WorldToPoint(Vector2 world)
	{
		var offseted = world+m_offset;

		int x = (int)(offseted.x / m_scale);
		int y = (int)(offseted.y / m_scale);

		return Point.Make (x,y);
	}

	public Vector2 PointToWorld(Point point)
	{
		var halfScale = 0.5f;

		return (new Vector2 (point.X+halfScale,point.Y+halfScale)*m_scale)-m_offset;
	}

}


