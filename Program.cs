using BookMS.Models;

BookManagementSystem bms = new BookManagementSystem();

// me not use TryParse, TryParse mean good practices >:(
string input = "";
while (input != "5")
{
    Console.WriteLine($"""
    Welcome to the Book Management System! 
    You have {bms.getBookCount()} books in your library.
        1. Display all books
        2. View book by ID
        3. Add a book
        4. Remove a book by ID
        5. Exit
    """);

    input = HelperFunctions.GetInput("Enter a number to select an option:");

    bool validID = true;
    do
    {
        switch (input)
        {
            case "1":
                bms.printLibrarybyID();
                break;
            case "2":
                validID = bms.tryGetBook();
                break;
            case "3":
                bms.userAddBook();
                break;
            case "4":
                validID = bms.userRemoveBook();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    } while (!validID);
}