namespace PokemonReview.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonType> PokemonTypes { get; set; }
    }
}
