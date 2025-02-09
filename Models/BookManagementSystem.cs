namespace BookMS.Models
{
    /// <summary>
    /// A book class, with title, author, and genre. IDs are in the BMS dictionary
    /// </summary>
    /// <author>Cameron Schultz</author>
    class Book
    {
        // ID is in BMS dictionary
        // Members optional only for abstraction reasons
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }

        public void printBookInfo()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Genre: " + Genre);
            Console.WriteLine("----------------------");
        }
    }


    /// <summary>
    /// Book Management System Class -- a library
    /// </summary>
    /// <author> Cameron Schultz </author>
    class BookManagementSystem
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();

        // To avoid setting outside of class
        private int bookCount = 0;
        public int getBookCount() { return bookCount; }

        /// <summary>
        /// Print all books stats in library, by ID
        /// </summary>
        /// <author>Cameron Schultz</author>
        public void printLibrarybyID()
        {
            Console.WriteLine("All books in Library:\n----------------------");

            if (bookCount < 1)
            {
                Console.WriteLine("No books in library.\n");
                return;
            }

            foreach (KeyValuePair<string, Book> book in books)
                {
                    // Separate because ID is not a member of book
                    Console.WriteLine("ID: " + book.Key);
                    book.Value.printBookInfo();
                }
        }

        /// <summary>
        /// Putting a new book into the system, using user input
        /// </summary>
        /// <author>Cameron Schultz</author>
        public void userAddBook()
        {
            Book newBook = new Book();
            newBook.Title = HelperFunctions.GetInput("Enter the title of the book:");
            newBook.Author = HelperFunctions.GetInput("Enter the author of the book:");
            newBook.Genre = HelperFunctions.GetInput("Enter the genre of the book:");
            string ID = HelperFunctions.GetInput("Enter the ID of the book:");

            books.Add(ID, newBook);
            bookCount++;
        }

        /// <summary>
        /// Remove a book from the system, using user-fed ID
        /// </summary>
        /// <returns>True if ID in dictionary</returns>
        /// <author>Cameron Schultz</author>
        public bool userRemoveBook()
        {
            string ID = HelperFunctions.GetInput("Enter the ID of the book you want to remove:");
            if (books.ContainsKey(ID))
            {
                Console.WriteLine("----------------------");
                books.Remove(ID);
                bookCount--;
                return true;
            }
            else
            {
                Console.WriteLine("Book not found.\n");
                return false;
            }
        }

        /// <summary>
        /// Attempt to get book info, using user-fed ID
        /// </summary>
        /// <returns>True if ID in dictionary</returns>
        /// <author>Cameron Schultz</author>
        public bool tryGetBook()
        {
            string ID = HelperFunctions.GetInput("Enter the ID of the book you want to view:");
            Console.WriteLine("----------------------");
            if (books.ContainsKey(ID))
            {
                books[ID].printBookInfo();
                return true;
            }
            else
            {
                Console.WriteLine("Book not found.");
                return false;
            }
        }
    }


    /// <summary>
    /// Functions that make programming a little easier
    /// </summary>
    /// <author>Cameron Schultz</author>
    class HelperFunctions
    {
        /// <summary>
        /// Get input safely
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <param name="required">If the input can be empty</param>
        /// <returns>string</returns>
        /// <author>Cameron Schultz</author>
        public static string GetInput(string prompt, bool required = true)
        {
            Console.WriteLine(prompt);

            string input = "";

            bool validInput = false;
            while (!validInput)
            {
                Console.Write("> ");

                input = Console.ReadLine();
                if (required && string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This field is required. Please enter again.");
                }
                else
                {
                    validInput = true;
                }
            }

            Console.WriteLine();
            return input;
        }
    }
}