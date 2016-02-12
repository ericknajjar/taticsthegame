using System;

//TODO: Composite Visitor
public static class IUnitCapabilitiesCompositeExtensions
{
	public static IUnitCapabilities Merge(this IUnitCapabilities a, IUnitCapabilities b)
	{
		return new CompositeCapability (a, b);
	}

	public class CompositeCapability: IUnitCapabilities
	{
		IUnitCapabilities m_a;
		IUnitCapabilities m_b;

		public CompositeCapability(IUnitCapabilities a, IUnitCapabilities b)
		{
			m_a = a;
			m_b = b;
		}

		public int Count {
			get {
				return m_a.Count+m_b.Count;
			}
		}
			
		public void Visit (IUnitCapabilityVisitor visitor)
		{
			m_a.Visit (visitor);
			m_b.Visit (visitor);
		}
	}
}


