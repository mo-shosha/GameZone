using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesService: ICategoriesService
    {
        private readonly AppDbContext _db;

        public CategoriesService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _db.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
