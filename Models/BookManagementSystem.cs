using System.Diagnostics.Contracts;
using Microsoft.VisualBasic.FileIO;

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
            if (bookCount < 1)
            {
                Console.WriteLine("No books in library.\n");
                return;
            }

            Console.WriteLine("All books in Library:\n----------------------");

            foreach (KeyValuePair<string, Book> ID_and_Book in books)
            {
                // Separate because ID is not a member of book
                Console.WriteLine("ID: " + ID_and_Book.Key);
                ID_and_Book.Value.printBookInfo();
            }
        }

        /// <summary>
        /// Putting a new book into the system, using user input
        /// </summary>
        /// <author>Cameron Schultz</author>
        public void userAddBook()
        {
            Book newBook = new Book();

            // Set the book's members
            newBook.Title = HelperFunctions.GetInput("Enter the title of the book:");
            newBook.Author = HelperFunctions.GetInput("Enter the author of the book:");
            newBook.Genre = HelperFunctions.GetInput("Enter the genre of the book:");

            string ID = "exit";

            // 'exit' cannot be used as an ID, it is reserved for exiting before a query
            while (ID.ToLower() == "exit")
            {
                ID = HelperFunctions.GetInput("Enter the ID of the book:");

                // 'exit' is a reserved word
                if (ID.ToLower() == "exit")
                {
                    Console.WriteLine("'exit' is a reserved word. Please enter a different ID.");
                }

                // Dictionary already contains book with ID
                else if (books.ContainsKey(ID))
                {
                    Console.WriteLine("ID already in use. Please enter a different ID.");
                    ID = "exit";
                }
            }

            // Confirm book addition (C# apparently takes care of stray instances)
            if (HelperFunctions.GetInput("Are you sure you want to add this book? (y/n)").ToLower() == "n")
            {
                return;
            }

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
            if (bookCount < 1)
            {
                Console.WriteLine("No books in library.\n");
                return true; // misleading, but if it were false it would ask continuously
            }

            string ID = HelperFunctions.GetInput("Enter the ID of the book you want to remove: (or 'exit' to return to main menu)");
            
            // Remove book info
            if (books.ContainsKey(ID) && (HelperFunctions.GetInput("Are you sure you want to remove this book? (y/n)").ToLower() == "y"))
            {
                Console.WriteLine("----------------------");
                books.Remove(ID);
                bookCount--;
                return true;
            }
            // Cancel transaction
            else if (ID == "exit") { return true; }
            // ID not found
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
            if (bookCount < 1)
            {
                Console.WriteLine("No books in library.\n");
                return true; // misleading, but if it were false it would ask continuously
            }

            string ID = HelperFunctions.GetInput("Enter the ID of the book you want to view: (or 'exit' to return to main menu)");

            // Print book info
            Console.WriteLine("----------------------");
            if (books.ContainsKey(ID))
            {
                books[ID].printBookInfo();
                return true;
            }
            // Cancel transaction
            else if (ID == "exit") { return true; }
            // ID not found
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