namespace QuanLyQuanNet.DTOs
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category() { }

        public Category(string categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}