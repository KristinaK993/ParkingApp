using System.Linq;
using Spectre.Console;

public class ParkingService : IParkingService
{
    private readonly Dictionary<string, decimal> _locationRates = new()
    {
        { "Centralen", 30.0m },
        { "Nordstan", 25.0m },
        { "Liseberg", 20.0m }
    };
    public void StartParking(string licensePlate, string location, int hours, User user)
    {
        if (!_locationRates.ContainsKey(location))
            throw new ArgumentException("Invalid location.");

        decimal rate = _locationRates[location];
        decimal cost = rate * hours;

        ParkingSession session = new()
        {
            LicensePlate = licensePlate,
            Location = location,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(hours),
            Cost = cost
        };

        user.ParkingHistory.Add(session);
        Console.WriteLine($"Parking started at {location}. Total cost: {cost} SEK.");
    }
    public void EndParking(User user)
    {
        var activeSession = user.ParkingHistory.LastOrDefault(s => s.EndTime > DateTime.Now);
        if (activeSession != null)
        {
            // Räkna ut faktisk tid som har passerat
            var actualDuration = DateTime.Now - activeSession.StartTime;
            var totalMinutes = (decimal)actualDuration.TotalMinutes;

            // Hämta priset per minut
            var ratePerHour = _locationRates[activeSession.Location];
            var ratePerMinute = ratePerHour / 60;

            // Dynamisk kostnadsberäkning
            var actualCost = Math.Round(ratePerMinute * totalMinutes, 2);

            // Visa resultat
            AnsiConsole.Markup($"[green]Parking ended for {activeSession.LicensePlate}.[/]\n");
            AnsiConsole.Markup($"Total time parked: [yellow]{Math.Floor(totalMinutes / 60)}h {totalMinutes % 60}min[/].\n");
            AnsiConsole.Markup($"You owe: [yellow]{actualCost} SEK[/].\n");

            // Uppdatera historik
            activeSession.EndTime = DateTime.Now;
            activeSession.Cost = actualCost;
        }
        else
        {
            AnsiConsole.Markup("[red]No active parking sessions to end.[/]\n");
        }
    }

    public void ShowHistory(User user)
    {
        if (!user.ParkingHistory.Any())
        {
            Console.WriteLine("No parking history available.");
            return;
        }
        Console.WriteLine("Parking History: ");
        foreach (var session in user.ParkingHistory)
        {
            Console.WriteLine($"- {session.LicensePlate} at {session.Location}, " +
                              $"from {session.StartTime} to {session.EndTime}, Cost: {session.Cost} SEK.");
        }
    }
    public Dictionary<string, decimal> GetParkingRates()
    {
        return _locationRates;
    }
}
