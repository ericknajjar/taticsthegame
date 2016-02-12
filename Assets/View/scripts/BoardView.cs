using UnityEngine;
using System.Collections;
using u3dExtensions.Engine.Runtime;
using u3dExtensions.Events;

public class BoardView : MonoBehaviour 
{
	[SerializeField]
	CellView m_cellViewPrfab = null;

	//[SerializeField]
	float m_scaleRation = 0.32f;

	CellView  [,]m_viewBoard;

	EventSlot<int,int> m_onCellClicked = new EventSlot<int, int>();
	Vector2 m_offset = Vector2.zero;

	public IEventRegister<int, int> OnCellClicked
	{
		get {
			return m_onCellClicked;
		}
	}


	[BindingProvider]
	public static BoardView CreateBoardView(int width, int height)
	{
		var prefab = Resources.Load<GameObject> ("BoardView");

		var go = (GameObject)GameObject.Instantiate (prefab);

		var ret = go.GetComponent<BoardView> ();

		ret.Init (width,height);

		return ret;
	}

	void Init(int width, int height)
	{
		m_viewBoard = new CellView[width, height];

		m_offset = new Vector2 (-width / 2.0f, -height / 2.0f)*m_scaleRation;

		for (int x = 0; x < width; ++x) 
		{
			for(int y = 0; y < height; ++y)
			{

				var pos = (new Vector2 (x, y) * m_scaleRation)+m_offset;
				var go = (GameObject)GameObject.Instantiate (m_cellViewPrfab.gameObject,pos,Quaternion.identity);
				m_viewBoard [x, y] = go.GetComponent<CellView> ();

			}
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 worldPos = ((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition))-m_offset;

			int x = (int)(worldPos.x / 0.32f);
			int y = (int)(worldPos.y / 0.32f);

			bool isXValid = x >= 0 && x < m_viewBoard.GetLength (0);
			bool isYValid = y >= 0 && y < m_viewBoard.GetLength (1);

			if (isXValid && isYValid) 
			{
				m_onCellClicked.Trigger (x,y);
			}

		}
	}

}
