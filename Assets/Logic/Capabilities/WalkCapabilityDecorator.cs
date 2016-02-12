using System;
using System.Collections.Generic;

//TODO: Decorator

public static class WalkCapabilityDecorator
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

		public IUnitCapabilities Capabilities {
			get {

				var possibleDestinations = new List<Point> (m_canWalkProvider);
				var command = new WalkCapability (m_decoratee, possibleDestinations);
				return m_decoratee.Capabilities.Merge (command);
			}
		}

		#endregion

		class WalkCapability: IUnitCapabilities, IWalkCapability
		{
			IList<Point> m_posibleDestinations;

			public WalkCapability(IGameUnit unit, IList<Point> posibleDestinations)
			{
				m_posibleDestinations = posibleDestinations;
			}

			#region IPickSinglePointCommand implementation
			public void Visit (IUnitCapabilityVisitor visitor)
			{
				visitor.WalkCapability (this);
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


