namespace WebAppOrder.Entities
{
    public sealed class Order
    {
        public int OrderNumber { get; set; }

        public string ItemName { get; set; }

        public double Price { get; set; }
    }
}
