namespace SmartParking
{
    public class ParkingLot
    {
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public int NumOfSpots { get; }

        private List<ParkingSpot> Spots_;
        public IReadOnlyList<ParkingSpot> Spots => Spots_.AsReadOnly();
        public Dictionary<string,List< ParkingSession>> SessionsHistory { get; set; }


        public ParkingLot(string name, decimal hourlyRate, int numOfSpots)
        {
            Name = name;
            HourlyRate = hourlyRate;
            NumOfSpots = numOfSpots;
            Spots_ = new List<ParkingSpot>();
            SessionsHistory = new Dictionary<string,List<ParkingSession>>();
        }
        public void AddSpot(ParkingSpot spot)
        {
            if (Spots_.Count >= NumOfSpots)
                throw new InvalidOperationException("Parking lot is full. Cannot add more spots.");

            Spots_.Add(spot);
            SessionsHistory[spot.SpotId] = new List<ParkingSession>();
        }
        public void AssignSpot(Vehicle vehicle)
        {
            var freeSpot = Spots.FirstOrDefault(s => s.IsAvailable());

            if (freeSpot == null)
                throw new InvalidOperationException("No available spots.");

            freeSpot.AssignVehicle(vehicle);
        }
        public decimal RemoveVehicle(string licensePlate)
        {
            var spot = Spots.FirstOrDefault(s => s.CurrentVehicle?.LicensePlate == licensePlate);

            if (spot == null || spot.CurrentVehicle == null || spot.CurrentSession == null)
                throw new InvalidOperationException("Vehicle not found.");

            var (session, fee) = spot.RemoveVehicle(HourlyRate);

            if (session == null)
                throw new InvalidOperationException("Error ending session.");

            RecordSession(session, spot.SpotId);
            return Math.Round(fee,2);
        }
        private void RecordSession(ParkingSession session,string spotId)
        {
            if (!SessionsHistory.ContainsKey(spotId))
            {
                SessionsHistory[spotId] = new List<ParkingSession>();
            }
            SessionsHistory[spotId].Add(session);
        }
    }
}
