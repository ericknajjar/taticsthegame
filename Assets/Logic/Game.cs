using System;
using u3dExtensions.Engine.Runtime;
using u3dExtensions;
using UnityEngine;
using u3dExtensions.IOC;
using u3dExtensions.IOC.extensions;
using u3dExtensions.Events;

public class Game: MonoBehaviour
{
	BoardView m_boardView;

	public Game(BoardView boardView)
	{
		m_boardView = boardView;

		m_boardView.OnCellClicked.Register ((x, y) => {

		
		});
	}

	[BindingProvider(DependencyCount = 1)]
	public static Game NewGame(IBindingContext context)
	{
		var boardView = context.Get<BoardView> (InnerBindingNames.Empty,10,10);

		return new Game(boardView);
	}
}


