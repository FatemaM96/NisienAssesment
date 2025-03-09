namespace MyNisienDrinkApi.Models{
 public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DrinkOrder> DrinkOrders { get; set; } = new();
        public List<DrinkRunParticipant> DrinkRunParticipants { get; set; } = new();
    }
}