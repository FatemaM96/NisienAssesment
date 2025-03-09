namespace MyNisienDrinkApi.Models
{
    public class DrinkRunParticipant
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string DrinkOrderId { get; set; }
        public User User { get; set; }
        public DrinkOrder DrinkOrder { get; set; }
    }
}
