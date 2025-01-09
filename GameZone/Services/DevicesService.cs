using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DevicesService: IDevicesService
    {
        private readonly AppDbContext _db;

        public DevicesService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _db.Devices
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .OrderBy(d => d.Text)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
