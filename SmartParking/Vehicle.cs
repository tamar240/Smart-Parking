namespace SmartParking
{
    public enum VehicleType
    {
        Car,          
        Motorcycle,
        Truck,         
        Bus,           
        Van,           
        ElectricCar,   
        HybridCar,     
        Scooter,       
        Bicycle,        
        Taxi,           
        Ambulance,    
        FireTruck       
    }
    public record Vehicle(string LicensePlate, VehicleType VehicleType);
}
