using static ElevadorSistemasSupervisorios.ElevatorSystem.ElevatorUtils;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class OnFloorChangedEventArgs
    {
        public int Floor { get; set; }

        public ElevatorDirection Direction { get; set; }
    }
}
