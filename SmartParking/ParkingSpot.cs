namespace SmartParking
{
    public class ParkingSpot
    {
        public string SpotId { get; set; }
        public Vehicle? CurrentVehicle { get; set; }
        public ParkingSession? CurrentSession { get; set; }

        public ParkingSpot(string spotId)
        {
            SpotId = spotId;
            CurrentVehicle = null;
            CurrentSession = null;
        }
        public bool IsAvailable() => CurrentVehicle == null;
        public void AssignVehicle(Vehicle vehicle)
        {
            CurrentVehicle = vehicle;
            CurrentSession = new ParkingSession(vehicle);
        }
        public (ParkingSession?,decimal) RemoveVehicle(decimal hourlyRate)
        {
            var session = CurrentSession;

            CurrentVehicle = null;
            CurrentSession = null;

            decimal fee = session?.EndSession(hourlyRate) ?? 0;

            return (session,fee);
        }
    }
}
