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
}