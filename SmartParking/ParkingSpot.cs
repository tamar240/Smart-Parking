using System.Diagnostics.Metrics;

namespace SmartParking
{
    public class ParkingSpot
    {
        private static int Counter;
        public int SpotId { get; set; }
        public Vehicle? CurrentVehicle { get; set; }
        public ParkingSession? CurrentSession { get; set; }
        public bool IsAvailable => CurrentVehicle == null;

        public ParkingSpot()
        {
            SpotId = ++Counter;
        }
      
        public void AssignVehicle(Vehicle vehicle)
        {
            CurrentVehicle = vehicle;
            CurrentSession = new (vehicle);
        }
        public (ParkingSession?,decimal) RemoveVehicle(decimal hourlyRate)
        {
            var session = CurrentSession;

            CurrentVehicle = null;
            CurrentSession = null;

            var fee = session?.EndSession(hourlyRate) ?? 0;

            return (session,fee);
        }
    }
}
