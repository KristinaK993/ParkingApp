public class User
{
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public List<ParkingSession> ParkingHistory { get; set; } = new List<ParkingSession>();

}
