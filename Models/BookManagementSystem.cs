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
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
    }

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
                tryPrintBook(ID_and_Book.Key);
            }
        }

        /// <summary>
        /// Putting a new book into the system, using user input
        /// </summary>
        public void userAddBook()
        {
            Book newBook = new Book();

            // Set the book's members
            Console.WriteLine("Enter the title of the book:");
            newBook.Title = HelperFunctions.GetInput();

            Console.WriteLine("Enter the author of the book:");
            newBook.Author = HelperFunctions.GetInput();

            Console.WriteLine("Enter the genre of the book:");
            newBook.Genre = HelperFunctions.GetInput();

            // Get a unique ID
            string ID = string.Empty;
            bool validID = false;
            while (!validID)
            {
                Console.WriteLine("Enter the ID of the book:");
                ID = HelperFunctions.GetInput();

                // Dictionary already contains book with ID
                if (books.ContainsKey(ID))
                {
                    Console.WriteLine("ID already in use. Please enter a different ID.");
                    continue;
                }

                validID = true;
            }

            Console.WriteLine("Are you sure you want to add this book? (y/n)");
            if (HelperFunctions.GetInput(true, true).ToLower() == "n")
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

            Console.WriteLine("Enter the ID of the book you want to remove: (leave blank to return)");
            string ID = HelperFunctions.GetInput(false);

            // Cancel transaction
            if (ID == string.Empty) { return true; }

            // Remove book info
            if (books.ContainsKey(ID))
            {
                tryPrintBook(ID);

                Console.WriteLine("Are you sure you want to remove this book? (y/n)");
                if (HelperFunctions.GetInput(true, true).ToLower() == "n")
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
        public bool tryPrintBook(string ID = "")
        {
            if (bookCount < 1)
            {
                Console.WriteLine("No books in library.");
                return true; // Exit successfully
            }

            if (ID == string.Empty)
            {
                Console.WriteLine("Enter the ID of the book you want to view: (leave empty to return)");
                ID = HelperFunctions.GetInput(false);
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
