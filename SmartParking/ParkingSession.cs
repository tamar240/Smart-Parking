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

        private decimal CalculateFee(decimal hourlyRate,int roundMinutes=15)
        {
            if (ExitTime == null)
                throw new InvalidOperationException("Session is still active.");

            var duration = ExitTime.Value - EntryTime;
            var totalMinutes = duration.TotalMinutes;

            var minutesToCharge = Math.Ceiling(totalMinutes / roundMinutes) * roundMinutes;


            Fee = (decimal)minutesToCharge * (hourlyRate / 60);
            return Fee;
        }
     
    }
}
