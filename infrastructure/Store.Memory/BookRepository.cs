using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12312-31231", "D. Knuth", "Art of Programing"),
            new Book(2, "ISBN 12312-31232", "S. Chacon, B. Straub", "Pro Git"),
            new Book(3,"ISBN 12312-31232", "A. Freeman" ,"Pro ASP.NET Core MVC 2"),
        };

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Author.Contains(query)
                                    || book.Title.Contains(query)).ToArray();
        }
    }
}
