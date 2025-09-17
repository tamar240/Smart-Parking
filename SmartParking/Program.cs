using SmartParking;

var lot = new ParkingLot("Lot A", 10m, 3);

lot.AddSpot(new ParkingSpot()).GetValueOrThrow();
lot.AddSpot(new ParkingSpot()).GetValueOrThrow();
lot.AddSpot(new ParkingSpot()).GetValueOrThrow();

var car1 = new Vehicle("123-456", VehicleType.Car);
var car2 = new Vehicle("789-101", VehicleType.Motorcycle);


var spot1 = lot.AssignSpot(car1).GetValueOrThrow();
var spot2 = lot.AssignSpot(car2).GetValueOrThrow();


Thread.Sleep(2000);

var fee1 = lot.RemoveVehicle("123-456").GetValueOrThrow();
Console.WriteLine($"Car1 fee: {fee1}");

var fee2 = lot.RemoveVehicle("789-101").GetValueOrThrow();
Console.WriteLine($"Car2 fee: {fee2}");

foreach (var spot in lot.Spots)
    Console.WriteLine($"Spot {spot.SpotId} has {lot.SessionsHistory[spot.SpotId].Count} session(s).");
