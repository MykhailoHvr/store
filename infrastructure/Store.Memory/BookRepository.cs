using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN1231231231", "D. Knuth", "Art of Programing", "This volume begins with basic programming concepts and techniques, then focuses more particularly on information structures-the representation of information inside a computer, the structural relationships between data elements and how to deal with them efficiently.",
                     7.19m),
            new Book(2, "ISBN1231231232", "S. Chacon, B. Straub", "Pro Git","Pro Git (Second Edition) is your fully-updated guide to Git and its usage in the modern world. Git has come a long way since it was first developed by Linus Torvalds for Linux kernel development. It has taken the open source world by storm since its inception in 2005, and this book teaches you how to use it like a pro.", 
                    56.99m),
            new Book(3,"ISBN1231231232", "A. Freeman" ,"Pro ASP.NET Core MVC 2","Now in its 7th edition, the best selling book on MVC is updated for ASP.NET Core MVC 2. It contains detailed explanations of the Core MVC functionality which enables developers to produce leaner, cloud optimized and mobile-ready applications for the .NET platform. This book puts ASP.NET Core MVC into context and dives deep into the tools and techniques required to build modern, cloud optimized extensible web applications. All the new MVC features are described in detail and the author explains how best to apply them to both new and existing projects.The ASP.NET Core MVC Framework is the latest evolution of Microsoft’s ASP.NET web platform, built on a completely new foundation. It represents a fundamental change to how Microsoft constructs and deploys web frameworks and is free of the legacy of earlier technologies such as Web Forms. ASP.NET Core MVC provides a 'host agnostic' framework and a high-productivity programming model that promotes cleaner code architecture, test-driven development, and powerful extensibility.",
                        52.35m),
        };

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                            join bookId in bookIds on book.Id equals bookId
                            select book;

            return foundBooks.ToArray();
        }

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Author.Contains(query)
                                    || book.Title.Contains(query)).ToArray();
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
