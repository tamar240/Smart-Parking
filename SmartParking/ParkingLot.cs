namespace SmartParking
{
    public class ParkingLot
    {
        public string ParkingLotName { get; set; }
        public decimal HourlyRate { get; set; }
        public int NumOfSpots { get; }

        private List<ParkingSpot> spots;
        private List<ParkingSpot> freeSpots;
        private Dictionary<string, ParkingSpot> vehicleToSpot;

        public IReadOnlyList<ParkingSpot> Spots => spots.AsReadOnly();

        public ParkingLot(string name, decimal hourlyRate, int numOfSpots)
        {
            if (hourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be greater than zero.", nameof(hourlyRate));

            if (numOfSpots <= 0)
                throw new ArgumentException("Number of spots must be greater than zero.", nameof(numOfSpots));

            ParkingLotName = name;
            HourlyRate = hourlyRate;
            NumOfSpots = numOfSpots;

            spots = new List<ParkingSpot>();
            freeSpots = new List<ParkingSpot>();
            vehicleToSpot = new Dictionary<string, ParkingSpot>();

            InitializeParkingSpots();
        }

        private void InitializeParkingSpots()
        {
            for (int i = 0; i < NumOfSpots; i++)
            {
                var spot = new ParkingSpot();
                spots.Add(spot);
                freeSpots.Add(spot);
            }
        }

        public OperationResult<ParkingSpot> AssignSpot(Vehicle vehicle)
        {
            if (freeSpots.Count == 0)
                return OperationResult<ParkingSpot>.Fail("No available spots.");

            var spot = freeSpots[0]; 
            freeSpots.RemoveAt(0);

            spot.AssignVehicle(vehicle);
            vehicleToSpot[vehicle.LicensePlate] = spot;

            return OperationResult<ParkingSpot>.Ok(spot, "Vehicle assigned successfully.");
        }

        public OperationResult<decimal> RemoveVehicle(string licensePlate)
        {
            if (!vehicleToSpot.TryGetValue(licensePlate, out var spot) || spot.CurrentVehicle == null || spot.CurrentSession == null)
                 return OperationResult<decimal>.Fail("Vehicle not found.");

            var fee = spot.RemoveVehicle(HourlyRate);

            freeSpots.Add(spot);
            vehicleToSpot.Remove(licensePlate);

            return OperationResult<decimal>.Ok(Math.Round(fee, 2), "Vehicle removed successfully.");
        }
    }
}
