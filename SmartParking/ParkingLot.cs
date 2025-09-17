namespace SmartParking
{
    public class ParkingLot
    {
        public string ParkingLotName { get; set; }
        public decimal HourlyRate { get; set; }
        public int NumOfSpots { get; }

        private List<ParkingSpot> Spots_;
        public IReadOnlyList<ParkingSpot> Spots => Spots_.AsReadOnly();
        public Dictionary<int, List<ParkingSession>> SessionsHistory { get; set; }//TODO - i want to move history in every spot


        public ParkingLot(string name, decimal hourlyRate, int numOfSpots)
        {
            ParkingLotName = name;
            HourlyRate = hourlyRate;
            NumOfSpots = numOfSpots;
            Spots_ = new();
            SessionsHistory = new();
        }
        public OperationResult<ParkingSpot> AddSpot(ParkingSpot spot)
        {
            if (Spots_.Count >= NumOfSpots)
                return OperationResult<ParkingSpot>.Fail("Parking lot is full. Cannot add more spots.");

            Spots_.Add(spot);
            SessionsHistory[spot.SpotId] = new();
            return OperationResult<ParkingSpot>.Ok(spot, "Spot added successfully.");
        }

        public OperationResult<ParkingSpot> AssignSpot(Vehicle vehicle)
        {
            var freeSpot = Spots.FirstOrDefault(s => s?.IsAvailable == true);

            if (freeSpot == null)
                return OperationResult<ParkingSpot>.Fail("No available spots."); 

            freeSpot.AssignVehicle(vehicle);
            return OperationResult<ParkingSpot>.Ok(freeSpot, "Vehicle assigned successfully.");
        }

        public OperationResult<decimal> RemoveVehicle(string licensePlate)
        {
            var spot = Spots.FirstOrDefault(s => s?.CurrentVehicle?.LicensePlate == licensePlate);

            if (spot == null || spot.CurrentVehicle == null || spot.CurrentSession == null)
                return OperationResult<decimal>.Fail("Vehicle not found.");

            var (session, fee) = spot.RemoveVehicle(HourlyRate);

            if (session == null)
                return OperationResult<decimal>.Fail("Error ending session.");

            RecordSession(session, spot.SpotId);
            return OperationResult<decimal>.Ok(Math.Round(fee, 2), "Vehicle removed successfully.");
        }

        private void RecordSession(ParkingSession session,int spotId)
        {
            if (!SessionsHistory.ContainsKey(spotId))
                SessionsHistory[spotId] = new();

            SessionsHistory[spotId].Add(session);
        }
    }
}
