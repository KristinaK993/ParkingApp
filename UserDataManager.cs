using System.Text.Json;

public static class UserDataManager
{
    public const string FilePath = "parking_data.json";

    public static void Initialize()
    {
        if (!File.Exists(FilePath))
        {
            var defaultUsers = new List<User>
            {
                new User
                {
                    Name = "Kiki",
                    CardNumber = "12345",
                    ParkingHistory = new List<ParkingSession>()

                }
            };
            SaveAllUsers(defaultUsers);
        }
    }
    public static List<User> LoadAllUsers()
    {
        if (!File.Exists(FilePath)) 
        {
            Console.WriteLine("JSON file not found.");
            return new List<User>();
        }

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public static void SaveAllUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Saved {users.Count} users to {FilePath}");
    }
    public static User FindUser(string cardNumber)
    {
        var users = LoadAllUsers();

       
        foreach (var user in users)
        {
            Console.WriteLine($"Checking user: {user.Name}, CardNumber: {user.CardNumber}");
        }

        var matchedUser = users.FirstOrDefault(user => user.CardNumber == cardNumber);

        if (matchedUser == null)
        {
            Console.WriteLine($"No match found for card number: {cardNumber}");
        }
        else
        {
            Console.WriteLine($"Match found: {matchedUser.Name}");
        }

        return matchedUser;
    }

    public static void SaveUser(User user)
    {
        var users = LoadAllUsers();
        var existingUser = users.FirstOrDefault(u => u.CardNumber == user.CardNumber);
        if (existingUser != null)
        {
            users.Remove(existingUser);
        }
        users.Add(user);
        SaveAllUsers(users);
    }


}