using SmartParking;

namespace SmartParkingTests
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]
        public void AddSpot_ShouldIncreaseSpotsCount()
        {
            var lot = new ParkingLot("Lot A", 10m, 3);
            lot.AddSpot(new ParkingSpot()).GetValueOrThrow();
            lot.AddSpot(new ParkingSpot()).GetValueOrThrow();

            Assert.AreEqual(2, lot.Spots.Count);
        }

        [TestMethod]
        public void AssignAndRemoveVehicle_ShouldCalculateFee()
        {
            var lot = new ParkingLot("Lot A", 10m, 3);
            var spot = lot.AddSpot(new ParkingSpot()).GetValueOrThrow();

            var car = new Vehicle("123-456", VehicleType.Car);
            lot.AssignSpot(car).GetValueOrThrow();

            Thread.Sleep(3000);

            var fee = lot.RemoveVehicle("123-456").GetValueOrThrow();
            Assert.IsTrue(fee > 0);

            Assert.AreEqual(1, lot.SessionsHistory[spot.SpotId].Count);
        }

        [TestMethod]
        public void SessionsHistory_ShouldTrackAllSessionsPerSpot()
        {
            var lot = new ParkingLot("Lot A", 10m, 3);
            var spot1 = lot.AddSpot(new ParkingSpot()).GetValueOrThrow();
            var spot2 = lot.AddSpot(new ParkingSpot()).GetValueOrThrow();

            var car1 = new Vehicle("123-456", VehicleType.Car);
            var car2 = new Vehicle("789-101", VehicleType.Motorcycle);

            lot.AssignSpot(car1).GetValueOrThrow();
            lot.AssignSpot(car2).GetValueOrThrow();

            Thread.Sleep(500);

            lot.RemoveVehicle("123-456").GetValueOrThrow();
            lot.RemoveVehicle("789-101").GetValueOrThrow();

            Assert.AreEqual(1, lot.SessionsHistory[spot1.SpotId].Count);
            Assert.AreEqual(1, lot.SessionsHistory[spot2.SpotId].Count);
        }
    }
}
