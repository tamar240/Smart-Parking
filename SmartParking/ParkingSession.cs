namespace SmartParking
{
    public class ParkingSession
    {
        public Vehicle Vehicle { get; set; }
        public DateTime EntryTime { get;}
        public DateTime? ExitTime { get; set; }
        public decimal Fee { get; set; }


        public ParkingSession(Vehicle vehicle)
        {
            Vehicle = vehicle;
            EntryTime = DateTime.Now;
            ExitTime = null;
            Fee = 0;
        }

        public decimal EndSession(decimal hourlyRate)
        {
            ExitTime = DateTime.Now;
            return CalculateFee(hourlyRate);
        }

        private decimal CalculateFee(decimal hourlyRate)
        {
            if (ExitTime == null)
                throw new InvalidOperationException("Session is still active.");

            var duration = ExitTime.Value - EntryTime;
            var totalMinutes = duration.TotalMinutes;

            Fee = (decimal)totalMinutes * (hourlyRate / 60);
            return Fee;
        }
    }
}
