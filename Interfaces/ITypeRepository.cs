namespace PokemonReview.Interfaces
{
    public interface ITypeRepository
    {
        ICollection<Models.Type> GetTypes();
        Models.Type GetType(int id);
        ICollection<Models.Type> GetTypeByName(string name);
    }
}
