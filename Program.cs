Console.WriteLine("Graph User Profile");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);


int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("1. Display my Profile information");
    Console.WriteLine("0. Exit");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    switch(choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            await GetUserProfileAsync();
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForUserAuth(settings,
        (info, cancel) =>
        {
            // Display the device code message to
            // the user. This tells them
            // where to go to sign in and provides the
            // code to use.
            Console.WriteLine(info.Message);
            return Task.FromResult(0);
        });
}

async Task GetUserProfileAsync()
{
    try
    {
        var user = await GraphHelper.GetUserAsync();
        Console.WriteLine($"\n");
        Console.WriteLine($"Name: {user?.DisplayName}");
        Console.WriteLine($"Email: {user?.Mail ?? user?.UserPrincipalName ?? ""}");
        Console.WriteLine($"Job Title: {user?.JobTitle}");
        Console.WriteLine($"Department: {user?.Department}");
        Console.WriteLine($"Email: {user?.Country}");
        Console.WriteLine($"User Type: {user?.UserType}");
        Console.WriteLine($"Account Created on: {user?.CreatedDateTime}");
        Console.WriteLine($"Account Enabled: {user?.AccountEnabled}");
        Console.WriteLine($"Password Last Set: {user?.LastPasswordChangeDateTime}");
        Console.WriteLine($"\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user: {ex.Message}");
    }
 
    
}

