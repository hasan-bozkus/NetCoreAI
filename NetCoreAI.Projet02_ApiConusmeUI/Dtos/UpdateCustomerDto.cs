namespace NetCoreAI.Projet02_ApiConusmeUI.Dtos
{
    public class UpdateCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public decimal CustomerBalance { get; set; }
    }
}
