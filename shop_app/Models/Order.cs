namespace shop_app.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // ID користувача, який зробив замовлення
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Стан замовлення (Pending, Completed, Canceled)
    }
}
