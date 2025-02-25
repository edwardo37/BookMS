using BookMS.Models;

ManagementSystem BMS = new ManagementSystem();

string input = string.Empty;
while (input != "5")
{
    Console.WriteLine($"""
    ----------------------
    Welcome to the Book Management System! 
    You have {BMS.getBookCount()} books in your library.
        1. Display all books
        2. View book by ID
        3. Add a book
        4. Remove a book by ID
        5. Exit
    """);

    Console.WriteLine("Enter a number to select an option:");
    input = HelperFunctions.GetInput();

    // Only some of these functions need a valid ID, so it continuously calls those until a valid ID is provided
    bool validID = true;
    do
    {
        switch (input)
        {
            case "1":
                BMS.printLibrarybyID();
                break;
            case "2":
                validID = BMS.tryPrintBook();
                break;
            case "3":
                BMS.userAddBook();
                break;
            case "4":
                validID = BMS.userRemoveBook();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    } while (!validID);
}