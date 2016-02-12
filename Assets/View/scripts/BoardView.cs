using UnityEngine;
using System.Collections;
using u3dExtensions.Engine.Runtime;

public class BoardView : MonoBehaviour 
{
	[SerializeField]
	CellView m_cellViewPrfab = null;

	//[SerializeField]
	float m_scaleRation = 0.32f;

	CellView  [,]m_viewBoard;


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

		Vector2 offeset = new Vector2 (-width / 2.0f, -height / 2.0f)*m_scaleRation;

		for (int x = 0; x < width; ++x) 
		{
			for(int y = 0; y < height; ++y)
			{

				var pos = (new Vector2 (x, y) * m_scaleRation)+offeset;
				var go = (GameObject)GameObject.Instantiate (m_cellViewPrfab.gameObject,pos,Quaternion.identity);
				m_viewBoard [x, y] = go.GetComponent<CellView> ();

			}
		}
	}

}
