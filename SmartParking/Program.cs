using SmartParking;

var lot = new ParkingLot("Lot A", 10m, 3);

var car1 = new Vehicle("123-456", VehicleType.Car);
var car2 = new Vehicle("789-101", VehicleType.Motorcycle);

var spot1 = lot.AssignSpot(car1);
var spot2 = lot.AssignSpot(car2);

Thread.Sleep(2000);

var fee1 = lot.RemoveVehicle("123-456");
Console.WriteLine($"Car1 fee: {(fee1.Success ? fee1.Data : fee1.Message)}");

var fee2 = lot.RemoveVehicle("789-101");
Console.WriteLine($"Car2 fee: {(fee2.Success ? fee2.Data : fee2.Message)}");

foreach (var spot in lot.Spots)
    Console.WriteLine($"Spot {spot.SpotId} has {spot.SessionsHistory.Count} session(s).");
