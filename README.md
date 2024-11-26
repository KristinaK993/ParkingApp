Parking App
A simple console-based parking management application that allows users to log in, register, start parking sessions, end parking, and view parking history. The project demonstrates core principles of C# programming, including object-oriented design, file handling, validation, and user-friendly console interaction using Spectre.Console.

Features
User Registration and Login:

Register new users with a name and a unique 5-digit card number.
Log in using the registered card number.
Parking Session Management:

Start a parking session by specifying:
License plate (validated for Swedish format: 3 letters followed by 3 digits).
Parking location (predefined options: Centralen, Nordstan, Liseberg).
Number of hours for parking.
End a parking session and calculate the total cost based on parking duration.
Parking History:

View a detailed history of all parking sessions, including:
License plate.
Parking location.
Start and end time.
Actual parking duration (hours and minutes).
Total cost.
Pricing:

Different hourly rates for different parking locations:
Centralen: 30 SEK/hour
Nordstan: 25 SEK/hour
Liseberg: 20 SEK/hour

Technical Overview
Technologies Used
C# .NET 8.0
Spectre.Console: For styled and interactive console output.
Figgle: For ASCII art in the console.
System.Text.Json: For JSON-based user data storage.
Project Structure
Program.cs: The entry point of the application. Handles user interaction and menu navigation.
User: Represents a user with properties like name, card number, and parking history.
ParkingSession: Represents a parking session with properties like license plate, location, start time, end time, and cost.
ParkingService: Handles parking-related operations such as starting, ending, and showing parking sessions.
UserDataManager: Manages user data, including loading and saving to a JSON file.
Validation: Ensures proper input validation, e.g., for Swedish license plates.
File Structure
parking_data.json: Stores user data persistently between application runs.
How to Run the Application
Clone the repository:https://github.com/KristinaK993/ParkingApp.git


Build and run the application:
bash
Kopiera kod
dotnet run
Follow the prompts to log in, register, and manage parking sessions.

Examples
Registration
less
Kopiera kod
Register a new account.
Enter your name: Kiki
Enter your 5-digit card number: 12345
[green]Registration successful![/]
Starting a Parking Session
markdown
Kopiera kod
Enter your license plate: ABC123
Select parking location:
  - Centralen
  - Nordstan
  - Liseberg
Enter number of hours to park: 3
[green]Parking has started successfully![/]
Ending a Parking Session
less
Kopiera kod
[green]Parking ended for: ABC123[/]
Total parking duration: [yellow]2 hours and 30 minutes[/]
Total cost: [yellow]75 SEK[/]
Viewing Parking History
yaml
Kopiera kod
[underline]Parking History[/]:
- ABC123 at Centralen, from 2024-11-25 10:00:00 to 2024-11-25 12:30:00, Duration: 2 hours and 30 minutes, Cost: 75 SEK
Validations
License plates must be in the format ABC123 (3 letters followed by 3 digits). Invalid formats will display an error message.
Card numbers must be exactly 5 digits.
Known Limitations
Currently, there’s no GUI interface; everything runs in the terminal.
Parking rates and locations are hardcoded.
Future Enhancements
Add a GUI or web interface for better user experience.
Allow dynamic pricing or custom locations.
Implement user authentication with passwords.
License
This project is licensed under the MIT License.
