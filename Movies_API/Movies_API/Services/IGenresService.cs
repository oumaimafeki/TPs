using Movies_API.Dtos;
using Movies_API.Models;

namespace Movies_API.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsvalidGenre(byte id);
        Task Add(GenreDto genre);
    }
}
