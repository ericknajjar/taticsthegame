using System;
using System.Collections.Generic;

//TODO: Decorator

public static class WalkCommandDecorator
{
	public static IGameUnit CanWalk(this IGameUnit unit, ICanWalkPointProvider canWalkProvider)
	{
		return new CanWalkDecorator (unit,canWalkProvider);
	}


	class CanWalkDecorator: IGameUnit
	{
		IGameUnit m_decoratee;
		ICanWalkPointProvider m_canWalkProvider;

		public CanWalkDecorator(IGameUnit unit, ICanWalkPointProvider canWalkProvider)
		{
			m_decoratee = unit;
			m_canWalkProvider = canWalkProvider;
		}

		#region IGameUnit implementation

		public ICommands Commands {
			get {

				var possibleDestinations = new List<Point> (m_canWalkProvider);
				var command = new WalkCommand (m_decoratee, possibleDestinations);
				return m_decoratee.Commands.Merge (command);
			}
		}

		#endregion

		class WalkCommand: ICommands, IPickSinglePointCommand
		{
			IList<Point> m_posibleDestinations;

			public WalkCommand(IGameUnit unit, IList<Point> posibleDestinations)
			{
				m_posibleDestinations = posibleDestinations;
			}

			#region IPickSinglePointCommand implementation
			public void Pick (int pointIndex)
			{

			}

			public void Visit (ICommandsVisitior visitor)
			{
				visitor.PickSinglePointCommand (this);
			}

			public IList<Point> AvailablePoints {
				get {
					return new List<Point>(m_posibleDestinations);
				}
			}
			#endregion

			#region ICommands implementation

			public int Count {
				get {
					return 1;
				}
			}

			#endregion

		}
	}
}


