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
        // Hitta en pågående parkering
        var activeSession = user.ParkingHistory.LastOrDefault(s => s.EndTime > DateTime.Now);

        if (activeSession != null)
        {
            // Beräkna parkeringens varaktighet
            TimeSpan parkingDuration = DateTime.Now - activeSession.StartTime; // Här använder vi aktuellt datum och tid för att beräkna parkeringens varaktighet

            // Beräkna den totala kostnaden baserat på varaktigheten
            decimal totalCost = activeSession.Cost * ((decimal)parkingDuration.TotalHours); // Räkna om kostnaden baserat på den faktiska parkeringstiden

            // Skriv ut resultatet
            AnsiConsole.Markup($"[green]Parking ended for: {activeSession.LicensePlate}[/]\n");
            AnsiConsole.Markup($"Total parking duration: [yellow]{parkingDuration.Hours} hours and {parkingDuration.Minutes} minutes[/]\n");
            AnsiConsole.Markup($"Total cost: [yellow]{totalCost:C2} SEK[/]\n");

            // Vänta på en tangenttryckning för att återgå till menyn
            AnsiConsole.Markup("[yellow]Press any key to return to the menu...[/]");
            Console.ReadKey(true);
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

        AnsiConsole.Markup("[underline]Parking History[/]:\n");

        foreach (var session in user.ParkingHistory)
        {
            // Beräkna parkeringens varaktighet
            TimeSpan parkingDuration = session.EndTime - session.StartTime;

            // Visa parkeringens historik
            AnsiConsole.Markup($"- [yellow]{session.LicensePlate}[/] at [green]{session.Location}[/], " +
                              $"from [yellow]{session.StartTime}[/] to [yellow]{session.EndTime}[/], " +
                              $"Duration: [yellow]{parkingDuration.Hours} hours and {parkingDuration.Minutes} minutes[/], " +
                              $"Cost: [green]{session.Cost} SEK[/]\n");
        }
    }




    public Dictionary<string, decimal> GetParkingRates()
    {
        return _locationRates;
    }
}
