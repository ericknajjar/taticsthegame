using System;
using System.Collections.Generic;


public class GameUnitSelectableAdapter: ISelectable
{
	IGameUnit m_unit;

	public GameUnitSelectableAdapter (IGameUnit unit)
	{
		m_unit = unit;
	}
		
	public ISelection Select (Point p, IBoardCommandFactory factory)
	{
		CommandBuilderCapabilityVisitor capabilityVisitor = new CommandBuilderCapabilityVisitor (p, factory);
		m_unit.Capabilities.Visit (capabilityVisitor);
		return new UnitSelection (capabilityVisitor.Commands);
	}

	class CommandBuilderCapabilityVisitor: IUnitCapabilityVisitor
	{
		Point m_origin;
		IBoardCommandFactory m_factory;

		public CommandBuilderCapabilityVisitor(Point origin,IBoardCommandFactory factory)
		{
			m_origin = origin;
			Commands = new List<IBoardCommand>();
			m_factory = factory;
		}

		public void WalkCapability (IWalkCapability capability)
		{
			var command = m_factory.BuildWalkCommand (m_origin, capability.AvailablePoints);
			Commands.Add (command);
		}			

		public List<IBoardCommand> Commands {
			get;
			private set;
		}
	}

	class UnitSelection: ISelection
	{
		public UnitSelection(List<IBoardCommand> commands)
		{
			Commands = BoardCommands.FromList(new List<IBoardCommand>(commands));
		}

		#region ISelection implementation

		public bool IsEmpty {
			get {
				return false;
			}
		}

		public IBoardCommands Commands {
			get ;
			private set;
		}

		#endregion
	}
}


