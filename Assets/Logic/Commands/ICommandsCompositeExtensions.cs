using System;

//TODO: Composite Visitor
public static class ICommandsCompositeExtensions
{
	public static ICommands Merge(this ICommands a, ICommands b)
	{
		return new CompositeCommands (a, b);
	}

	public class CompositeCommands: ICommands
	{
		ICommands m_a;
		ICommands m_b;

		public CompositeCommands(ICommands a, ICommands b)
		{
			m_a = a;
			m_b = b;
		}

		public int Count {
			get {
				return m_a.Count+m_b.Count;
			}
		}
			
		public void Visit (ICommandsVisitior visitor)
		{
			m_a.Visit (visitor);
			m_b.Visit (visitor);
		}
	}
}


