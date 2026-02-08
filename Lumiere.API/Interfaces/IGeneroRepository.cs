using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> GetGeneros();
        Genero GetGeneroById(int id);
        void AddGenero(Genero genero);
        void UpdateGenero(Genero genero);
        void DeleteGenero(int id);
        bool GeneroExists(int id);
    }
}