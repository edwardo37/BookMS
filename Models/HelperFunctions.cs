namespace BookMS.Models
{
    /// <summary>
    /// Functions (not plural in this case) that make programming a little easier
    /// </summary>
    /// <author>Cameron Schultz</author>
    class HelperFunctions
    {
        /// <summary>
        /// Get user input safely, with the option to require input and/or limit to 'y' or 'n'
        /// </summary>
        /// <param name="required">If the input can be empty</param>
        /// <param name="yn">If the input should be 'y' or 'n'</param>
        /// <returns>The user input string</returns>
        public static string GetInput(bool required = true, bool yn = false)
        {
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