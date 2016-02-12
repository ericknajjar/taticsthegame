using UnityEngine;
using System.Collections;
using u3dExtensions.Events;

//TODO: Introduzir um objeto Turno
public class TurnManager  {

	EventSlot<int> m_onTurnStarted = new EventSlot<int>();

	public IEventRegister<int> OnTurnStarted
	{
		get
		{
			return m_onTurnStarted;
		}
	}
}
