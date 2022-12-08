namespace DTOs
{
    public class LocationListItem
    {
         public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<EmployeeListItem>? Employees { get; set; }
    }
}