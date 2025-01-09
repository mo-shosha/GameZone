using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface IGamesService
    {
        Game? Get(int? id);
        Task<IEnumerable<Game>> GetAll();
        Task<Game?> Update(EditGameFormViewModel model);
        Task Create(CreateGameFormViewModel model); 
        bool Delete(int id);
    }
}
