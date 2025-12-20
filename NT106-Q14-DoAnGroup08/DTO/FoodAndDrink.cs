using System;

namespace QuanLyQuanNet.DTOs
{
    public class FoodAndDrink
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; }
        public DateTime CreateAt { get; set; }

        public FoodAndDrink() { }

        public FoodAndDrink(
            string foodId,
            string foodName,
            decimal price,
            string categoryId,
            string image,
            bool available,
            DateTime createdAt)
        {
            this.FoodId = foodId;
            this.FoodName = foodName;
            this.Price = price;
            this.CategoryId = categoryId;
            this.Image = image;
            this.Available = available;
            this.CreateAt = createdAt;
        }


    }
}