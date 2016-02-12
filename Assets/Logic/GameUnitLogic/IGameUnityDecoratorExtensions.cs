using System;

public static class IGameUnityDecoratorExtensions
{
	public static IGameUnit CanWalk(this IGameUnit unit)
	{
		return new CanWalkDecorator (unit);
	}

	class CanWalkDecorator: IGameUnit
	{
		IGameUnit m_decoratee;

		public CanWalkDecorator(IGameUnit unit)
		{
			m_decoratee = unit;
		}

		#region IGameUnit implementation

		public ICommands Commands {
			get {
				return m_decoratee.Commands.Merge (new WalkCommand ());
			}
		}

		#endregion

		class WalkCommand: ICommands
		{
			
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


