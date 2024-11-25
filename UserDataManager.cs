using System.Text.Json;

public static class UserDataManager
{
    private const string FilePath = "parking_data.json";

    public static void Initialize()
    {
        if (!File.Exists(FilePath))
        {
            var defaultUsers = new List<User>
            {
                new User
                {
                    Name = "Admin",
                    CardNumber = "12345",
                    ParkingHistory = new List<ParkingSession>()

                }
            };
            SaveAllUsers(defaultUsers);
        }
    }
    public static List<User> LoadAllUsers()
    {
        if (File.Exists(FilePath)) return new List<User>();
       string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
    public static void SaveAllUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(FilePath, json);
    }
    public static User FindUser(string cardNumber)
    {
        var users = LoadAllUsers();
        return users.FirstOrDefault(user => user.CardNumber == cardNumber);
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