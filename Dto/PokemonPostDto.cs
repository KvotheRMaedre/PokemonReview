namespace PokemonReview.Dto
{
    public class PokemonPostDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
    }
}
