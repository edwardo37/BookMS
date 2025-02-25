using BookMS.Models;

ManagementSystem BMS = new ManagementSystem();

Console.WriteLine(
    """
    ----------------------
    Welcome to the Book Management System!
    """);

string input = string.Empty;
while (input != "5")
{
    Console.WriteLine($"""
    ----------------------
    You have {BMS.returnBookCount()} books in your library.
    1. Display all books
    2. View book by ID
    3. Add a book
    4. Remove a book by ID
    5. Exit
    ----------------------
    """);

    Console.WriteLine("Enter a number to select an option:");
    input = HelperFunctions.GetInput();

    switch (input)
    {
        case "1":
            BMS.printLibrarybyID();
            break;
        case "2":
            BMS.tryPrintBook();
            break;
        case "3":
            BMS.userAddBook();
            break;
        case "4":
            BMS.userRemoveBook();
            break;
        case "5":
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}