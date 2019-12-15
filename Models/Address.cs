namespace api.Models {
    public class Address {
        public int Id { get; set; }
        public string ClientAddresss { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}