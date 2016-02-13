using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.Events;

public class BoardView : MonoBehaviour 
{
	[SerializeField]
	CellView m_cellViewPrfab = null;

	[SerializeField]
	GameObject m_unitPrefab = null;

	CellView  [,]m_viewBoard;

	Dictionary<Point,GameObject> m_units = new Dictionary<Point, GameObject> ();

	EventSlot<int,int> m_onCellClicked = new EventSlot<int, int>();

	//TODO: Transformar em um state Pattern?
	bool m_lockInput = false;

	WorldLogicCoordinateTransform m_transformer;

	public IEventRegister<int, int> OnCellClicked
	{
		get {
			return m_onCellClicked;
		}
	}

	[BindingProvider]
	public static BoardView CreateBoardView(int width, int height,WorldLogicCoordinateTransform transformer)
	{
		var prefab = Resources.Load<GameObject> ("BoardView");

		var go = (GameObject)GameObject.Instantiate (prefab);

		var ret = go.GetComponent<BoardView> ();
		ret.m_transformer = transformer;
		ret.Init (width,height);

		return ret;
	}

	void Init(int width, int height)
	{
		m_viewBoard = new CellView[width, height];

		for (int x = 0; x < width; ++x) 
		{
			for(int y = 0; y < height; ++y)
			{
				var point = Point.Make (x,y);

				var pos = m_transformer.PointToWorld (point);

				var go = (GameObject)GameObject.Instantiate (m_cellViewPrfab.gameObject,pos,Quaternion.identity);
				m_viewBoard [x, y] = go.GetComponent<CellView> ();

			}
		}
	}
		
	public void AddUnitView(Point p)
	{
		var pos = m_transformer.PointToWorld (p);
		var go = (GameObject)GameObject.Instantiate (m_unitPrefab,pos,Quaternion.identity);
		m_units.Add (p, go);
	}

	public void AddResult(ICommandResult result)
	{
		//TODO: Botar um visitor?
		var unitMoves = result as AUnitMoved;

		if (unitMoves != null) 
		{
			m_lockInput = true;
			StartCoroutine (MoveUnit (unitMoves.From,unitMoves.To));
		}
	}

	IEnumerator MoveUnit(Point from,Point to)
	{
		GameObject target = null;
		if(m_units.TryGetValue(from,out target))
		{
			m_units.Remove (from);
			m_units.Add (to, target);
			var worldPos = m_transformer.PointToWorld (to);

			for(;;)
			{
				target.transform.position = Vector3.Lerp (target.transform.position, worldPos, Time.deltaTime);

				if (Vector2.Distance (target.transform.position,worldPos) <= 0.01f) 
				{
					target.transform.position = worldPos;
					break;
				}

				yield return null;
			}

		}

		m_lockInput = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0) && !m_lockInput) 
		{
			
			Vector2 worldPos = ((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition));
			var point  = m_transformer.WorldToPoint (worldPos);
			int x = point.X;
			int y = point.Y;

			bool isXValid = x >= 0 && x < m_viewBoard.GetLength (0);
			bool isYValid = y >= 0 && y < m_viewBoard.GetLength (1);

			if (isXValid && isYValid) 
			{
				m_onCellClicked.Trigger (x,y);
			}

		}
	}

}
