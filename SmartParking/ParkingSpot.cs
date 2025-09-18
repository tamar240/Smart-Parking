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
        public List<ParkingSession> SessionsHistory { get;} 

        public ParkingSpot()
        {
            SpotId = ++Counter;
            SessionsHistory = new List<ParkingSession>();
        }
      
        public void AssignVehicle(Vehicle vehicle)
        {
            CurrentVehicle = vehicle;
            CurrentSession = new (vehicle);
        }

        public decimal RemoveVehicle(decimal hourlyRate)
        {
            var session = CurrentSession;

            CurrentVehicle = null;
            CurrentSession = null;

            var fee = session?.EndSession(hourlyRate) ?? 0;
            SessionsHistory.Add(session!);

            return fee;
        }
    }
}
