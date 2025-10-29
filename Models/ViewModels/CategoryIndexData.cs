namespace Pop_Raluca_Laborator2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<BookCategory> BookCategories { get; set; }
    }
}
