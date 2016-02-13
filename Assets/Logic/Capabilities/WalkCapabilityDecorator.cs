using System;
using System.Collections.Generic;

//TODO: Decorator

public static class WalkCapabilityDecorator
{
	public static IGameUnit CanWalk(this IGameUnit unit, int width, int height)
	{
		return new CanWalkDecorator (unit,width,height);
	}
		
	class CanWalkDecorator: IGameUnit
	{
		IGameUnit m_decoratee;
		int m_width;
		int m_height;

		public CanWalkDecorator(IGameUnit unit,int width, int height)
		{
			m_decoratee = unit;
			m_width = width;
			m_height = height;
		}

		#region IGameUnit implementation

		public IUnitCapabilities Capabilities {
			get {
				var command = new WalkCapability (m_width,m_height);
				return m_decoratee.Capabilities.Merge (command);
			}
		}

		#endregion

		class WalkCapability: IUnitCapabilities, IWalkCapability
		{
			
			public WalkCapability(int width, int height)
			{
				Width = width;
				Height = height;
			}

			public int Width {
				get;
				private set;
			}

			public int Height {
				get;
				private set;
			}

			public int Count {
				get {
					return 1;
				}
			}				
		
			public void Visit (IUnitCapabilityVisitor visitor)
			{
				visitor.WalkCapability (this);
			}


		}
	}
}


