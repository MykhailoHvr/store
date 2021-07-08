namespace Store
{
    public interface IBookRepository
    {
        Book[] GetAllByIsbn(string isbn);

        Book[] GetAllByTitleOrAuthor(string titleAuthor);

        Book GetById(int id);
    }
}
