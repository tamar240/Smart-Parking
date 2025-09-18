using SmartParking;

namespace SmartParkingTests
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]
        public void AddParkingLot_InitializeParkingSpot()
        {
            var lot = new ParkingLot("Lot A", 10m, 3);
            Assert.AreEqual(3, lot.Spots.Count);
        }

        [TestMethod]
        public void AssignSpot_WhenLotFull_ShouldReturnFail()
        {
            var lot = new ParkingLot("Lot A", 10m, 1); 
            var car1 = new Vehicle("123-456", VehicleType.Car);
            var car2 = new Vehicle("789-101", VehicleType.Car);

            lot.AssignSpot(car1);
            var result = lot.AssignSpot(car2);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("No available spots.", result.Message);
        }

        [TestMethod]
        public void RemoveVehicle_WhenVehicleNotFound_ShouldReturnFail()
        {
            var lot = new ParkingLot("Lot A", 10m, 1);
            var result = lot.RemoveVehicle("000-000");

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Vehicle not found.", result.Message);
        }

        [TestMethod]
        public void RemoveVehicle_ShouldCalculateFeeCorrectly()
        {
            var lot = new ParkingLot("Lot A", 10m, 1);
            var car = new Vehicle("123-456", VehicleType.Car);

            lot.AssignSpot(car);
            Thread.Sleep(1000); 
            var result = lot.RemoveVehicle("123-456");

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data > 0); 
        }

        [TestMethod]
        public void Spot_ShouldHaveHistoryAfterVehicleLeaves()
        {
            var lot = new ParkingLot("Lot A", 10m, 1);
            var car = new Vehicle("123-456", VehicleType.Car);

            var spot = lot.AssignSpot(car);
            lot.RemoveVehicle("123-456");

            Assert.IsNotNull(spot.Data); 
            Assert.AreEqual(1, spot.Data.SessionsHistory.Count);
        }

    }
}
