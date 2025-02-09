namespace BookMS.Models
{
    /// <summary>
    /// A book class, with title, author, and genre. IDs are in the BMS dictionary
    /// </summary>
    /// <author>Cameron Schultz</author>
    public class Book
    {
        // ID is in BMS dictionary
        // Members optional only for abstraction reasons
        private string? Title { get; set; }
        private string? Author { get; set; }
        private string? Genre { get; set; }


        /// <summary>
        /// Book Management System Class -- a library
        /// </summary>
        /// <remarks>Nested within Book to be able to access its members</remarks>
        /// <author>Cameron Schultz</author>
        public class ManagementSystem
        {
            private Dictionary<string, Book> books = new Dictionary<string, Book>();

            // To avoid setting outside of class
            private int bookCount = 0;
            public int getBookCount() { return bookCount; }

            /// <summary>
            /// Print all books stats in library, by ID
            /// </summary>
            public void printLibrarybyID()
            {
                if (bookCount < 1)
                {
                    Console.WriteLine("No books in library.");
                    return;
                }

                Console.WriteLine("All books in Library:");

                foreach (KeyValuePair<string, Book> ID_and_Book in books)
                {
                    tryGetBook(ID_and_Book.Key);
                }
            }

            /// <summary>
            /// Putting a new book into the system, using user input
            /// </summary>
            public void userAddBook()
            {
                Book newBook = new Book();

                // Set the book's members
                newBook.Title = HelperFunctions.GetInput("Enter the title of the book:");
                newBook.Author = HelperFunctions.GetInput("Enter the author of the book:");
                newBook.Genre = HelperFunctions.GetInput("Enter the genre of the book:");

                // Get a unique ID
                string ID = string.Empty;
                bool validID = false;
                while (!validID)
                {
                    ID = HelperFunctions.GetInput("Enter the ID of the book:");

                    // Dictionary already contains book with ID
                    if (books.ContainsKey(ID))
                    {
                        Console.WriteLine("ID already in use. Please enter a different ID.");
                        continue;
                    }

                    validID = true;
                }

                // Confirm book addition (C# apparently takes care of stray instances)
                if (HelperFunctions.GetInput("Are you sure you want to add this book? (y/n)", true, true).ToLower() == "n")
                {
                    return;
                }

                books.Add(ID, newBook);
                bookCount++;
            }

            /// <summary>
            /// Remove a book from the system, using user-fed ID
            /// </summary>
            /// <returns>Bool telling if ID in dictionary</returns>
            public bool userRemoveBook()
            {
                if (bookCount < 1)
                {
                    Console.WriteLine("No books in library.");
                    return true; // Exit successfully
                }

                string ID = HelperFunctions.GetInput("Enter the ID of the book you want to remove: (leave blank to return)", false);

                // Cancel transaction
                if (ID == string.Empty) { return true; }

                // Remove book info
                if (books.ContainsKey(ID))
                {
                    tryGetBook(ID);

                    if (HelperFunctions.GetInput("Are you sure you want to remove this book? (y/n)", true, true).ToLower() == "n")
                    {
                        return true; // Removal cancelled successfully
                    }

                    books.Remove(ID);
                    bookCount--;
                    Console.WriteLine("Book removed Succefully.");
                    return true;
                }
                // ID not found
                else
                {
                    Console.WriteLine("Book not found.");
                    return false;
                }
            }

            /// <summary>
            /// Attempt to get book info, using user-fed ID
            /// </summary>
            /// <returns>Bool telling if ID in dictionary</returns>
            public bool tryGetBook(string ID = "")
            {
                if (bookCount < 1)
                {
                    Console.WriteLine("No books in library.");
                    return true; // Exit successfully
                }

                if (ID == string.Empty)
                {
                    ID = HelperFunctions.GetInput("Enter the ID of the book you want to view: (leave empty to return)", false);
                }

                // Cancel transaction
                // Looks suspicious, but only runs if ID is empty *after* user input
                if (ID == string.Empty) { return true; }

                // Print book info
                if (books.ContainsKey(ID))
                {
                    Console.WriteLine("->------------------<-");
                    Console.WriteLine($"Title: {books[ID].Title}");
                    Console.WriteLine($"Author: {books[ID].Author}");
                    Console.WriteLine($"Genre: {books[ID].Genre}");
                    Console.WriteLine($"ID: {ID}");
                    Console.WriteLine("->------------------<-");
                    return true;
                }
                // ID not found
                else
                {
                    Console.WriteLine("Book not found.");
                    return false;
                }
            }
        }

    }

    /// <summary>
    /// Functions (not plural in this case) that make programming a little easier
    /// </summary>
    /// <author>Cameron Schultz</author>
    class HelperFunctions
    {
        /// <summary>
        /// Get user input safely, with the option to require input and/or limit to 'y' or 'n'
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <param name="required">If the input can be empty</param>
        /// <param name="yn">If the input should be 'y' or 'n'</param>
        /// <returns>The user input string</returns>
        public static string GetInput(string prompt, bool required = true, bool yn = false)
        {
            Console.WriteLine(prompt);

            string input = string.Empty;

            bool validInput = false;
            while (!validInput)
            {
                Console.Write("> ");

                // Will only actually be empty if required is false
                input = Console.ReadLine() ?? string.Empty;

                if (required && string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This field is required. Please enter again.");
                }
                else if (yn && input.ToLower() != "y" && input.ToLower() != "n")
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
                else
                {
                    validInput = true;
                }
            }

            return input;
        }
    }
}
