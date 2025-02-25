namespace BookMS.Models
{
    /// <summary>
    /// Book Management System Class -- a library
    /// </summary>
    /// <remarks>Nested within Book to be able to access its members</remarks>
    /// <author>Cameron Schultz</author>
    public class BookManagementSystem
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();

        // Program.cs needs the book count, for reporting it to the user
        public int returnBookCount() { return books.Count; }

        /// <summary>
        /// Print all books stats in library, by ID
        /// </summary>
        public void printLibrarybyID()
        {
            if (books.Count < 1)
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
            if (HelperFunctions.GetInput(true, true).ToLower() == "n") { return; /* Cancelled successfully */ }

            books.Add(ID, newBook);
        }

        /// <summary>
        /// Remove a book from the system, using user-fed ID
        /// </summary>
        public void userRemoveBook()
        {
            if (books.Count < 1)
            {
                Console.WriteLine("No books in library.");
                return; // Exit successfully
            }

            // Continues until a valid book is printed or the user aborts
            // Normally while (true) is a bad idea, but all paths will return
            while (true)
            {
                Console.WriteLine("Enter the ID of the book you want to remove: (leave blank to return)");
                string ID = HelperFunctions.GetInput(false);

                // Cancel transaction
                if (ID == string.Empty) { return; }

                // Remove book info
                if (books.ContainsKey(ID))
                {
                    tryPrintBook(ID);

                    Console.WriteLine("Are you sure you want to remove this book? (y/n)");
                    if (HelperFunctions.GetInput(true, true).ToLower() == "n")
                    {
                        return; // Removal cancelled successfully
                    }

                    books.Remove(ID);
                    Console.WriteLine("Book removed Succefully.");
                    return;
                }

                // ID not found
                Console.WriteLine("Book not found.");
            }
        }

        /// <summary>
        /// Attempt to get book info, using user-fed ID
        /// </summary>
        /// <returns>Bool telling if ID in dictionary</returns>
        public void tryPrintBook(string ID = "")
        {
            if (books.Count < 1)
            {
                Console.WriteLine("No books in library.");
                return;
            }

            // Again, while (true) is usually a bad idea, but all paths return
            while (true)
            {
                if (ID == string.Empty)
                {
                    Console.WriteLine("Enter the ID of the book you want to view: (leave empty to return)");
                    ID = HelperFunctions.GetInput(false);
                }

                // Cancel transaction, if the string is STILL empty
                if (ID == string.Empty) { return; }

                // Print book info
                if (books.ContainsKey(ID))
                {
                    Console.WriteLine("->------------------<-");
                    Console.WriteLine($"Title: {books[ID].Title}");
                    Console.WriteLine($"Author: {books[ID].Author}");
                    Console.WriteLine($"Genre: {books[ID].Genre}");
                    Console.WriteLine($"ID: {ID}");
                    Console.WriteLine("->------------------<-");
                    return;
                }

                // ID not found
                Console.WriteLine("Book not found.");
                // To restart loop
                ID = string.Empty;
            }
        }
    }
}
