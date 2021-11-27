using static ElevadorSistemasSupervisorios.ElevatorSystem.ElevatorUtils;

namespace ElevadorSistemasSupervisorios.ElevatorSystem
{
    public class OnGeneratedFloorEventArgs
    {
        public int Floor { get; set; }

        public ElevatorDirection Direction { get; set; }
    }
}
