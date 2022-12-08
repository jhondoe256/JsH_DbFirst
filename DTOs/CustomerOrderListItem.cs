namespace DTOs
{
    public class CustomerOrderListItem
    {
        public DateTime OrderDate { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? MealName { get; set; }
    }
}