public interface IParkingService
{
    void StartParking(string licensePlate, string location, int hours, User user);
    void EndParking(User user);
    void ShowHistory(User user);
    Dictionary<string, decimal> GetParkingRates();
}