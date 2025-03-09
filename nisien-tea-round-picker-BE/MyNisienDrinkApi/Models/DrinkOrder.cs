namespace MyNisienDrinkApi.Models
{
    public class DrinkOrder
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public AdditionalSpecification AdditionalSpecification { get; set; }
        public List<DrinkRunParticipant> DrinkRunParticipants { get; set; } = new();
    }

    public class AdditionalSpecification
    {
        public string AdditionalProp1 { get; set; }
        public string AdditionalProp2 { get; set; }
        public string AdditionalProp3 { get; set; }
    }
}
