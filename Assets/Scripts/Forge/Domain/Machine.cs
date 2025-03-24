namespace Forge.Domain
{
    public class Machine
    {
        public MachineTemplate Template { get; }

        public Machine(MachineTemplate template)
        {
            Template = template;
        }
    }
}