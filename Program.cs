using Figgle;
using Spectre.Console;

public static class Program
{
    static void Main(string[] args)
    {
        UserDataManager.Initialize();
        var parkingService = new ParkingService();

        User user = null;
        while (user == null)
        {
            AnsiConsole.WriteLine(FiggleFonts.Standard.Render("Parking App"));

            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to [green]Log in[/] or [blue]Register[/]?")
                    .AddChoices("Log in", "Register"));

            if (action == "Log in")
            {
                user = Login();
            }
            else if (action == "Register")
            {
                user = RegisterUser();
            }
        }

        MainMenu(user, parkingService);
    }

    static User Login()
    {
        string cardNumber = AnsiConsole.Ask<string>("Enter your [green]card number[/]:").Trim();

        var user = UserDataManager.FindUser(cardNumber);

        if (user == null)
        {
            AnsiConsole.Markup("[red]Invalid card number. Please try again.[/]\n");
            return null;
        }

        AnsiConsole.Markup($"[green]Welcome back, {user.Name}![/]\n");
        return user;
    }


    static User RegisterUser()
    {
        AnsiConsole.WriteLine("Register a new account.");

        var name = AnsiConsole.Ask<string>("Enter your [green]name[/]:");
        var cardNumber = AnsiConsole.Ask<string>("Enter your [green]5-digit card number[/]:");

        User user = new() { Name = name, CardNumber = cardNumber };
        var validator = new UserValidator();
        var result = validator.Validate(user);

        if (!result.IsValid)
        {
            AnsiConsole.Markup("[red]Invalid data![/]\n");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            return RegisterUser();
        }

        UserDataManager.SaveUser(user);
        AnsiConsole.Markup("[green]Registration successful![/]\n");
        return user;
    }

    static void MainMenu(User user, ParkingService parkingService)
    {
        while (true)
        {
            AnsiConsole.WriteLine(FiggleFonts.Standard.Render("Parking App"));

            // Visa parkeringspriserna
            AnsiConsole.Markup("[underline]Parking Rates[/]:\n");
            foreach (var rate in parkingService.GetParkingRates())
            {
                AnsiConsole.Markup($"- [yellow]{rate.Key}[/]: [green]{rate.Value} SEK/hour[/]\n");
            }
            AnsiConsole.WriteLine();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("Start Parking", "End Parking", "Show History", "Exit"));

            switch (choice)
            {
                case "Start Parking":
                    StartParking(parkingService, user);
                    break;
                case "End Parking":
                    parkingService.EndParking(user);
                    break;
                case "Show History":
                    parkingService.ShowHistory(user);
                    break;
                case "Exit":
                    UserDataManager.SaveUser(user);
                    return;
            }
        }
    }

    static void StartParking(ParkingService parkingService, User user)
    {
        var licensePlate = AnsiConsole.Ask<string>("Enter your [green]license plate[/]:");
        var location = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select parking location:")
                .AddChoices("Centralen", "Nordstan", "Liseberg"));
        var hours = AnsiConsole.Ask<int>("Enter [green]number of hours[/] to park:");

        try
        {
            parkingService.StartParking(licensePlate, location, hours, user);
            AnsiConsole.Markup("\n[green]Parking has started successfully![/]\n");
            AnsiConsole.Markup("[yellow]Press any key to return to the menu...[/]");
            Console.ReadKey(true); // Vänta på en knapptryckning innan vi återgår till menyn.
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[red]Error:[/] {ex.Message}\n");
            AnsiConsole.Markup("[yellow]Press any key to return to the menu...[/]");
            Console.ReadKey(true); // Vänta på en knapptryckning om det sker ett fel.
        }
    }

}
