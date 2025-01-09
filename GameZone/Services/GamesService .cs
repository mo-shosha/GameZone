using GameZone.Data;
using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameZone.Settings;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHost;
        private readonly string _imgPath;
        public GamesService(AppDbContext db, IWebHostEnvironment webHost)
        {
            _db = db;
            _webHost = webHost;
            _imgPath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name=model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=CoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };
            _db.Add(game);
            await _db.SaveChangesAsync();

        }

        public Game? Get(int? id)
        {
            return _db.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _db.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _db.Games
            .Include(g => g.Devices)
            .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _db.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imgPath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_imgPath, game.Cover);
                File.Delete(cover);

                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _db.Games.Find(id);  

            if (game is null)
                return isDeleted;

            _db.Remove(game);
            var effectedRows = _db.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(_imgPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }


        private async Task<string> SaveCover(IFormFile  Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imgPath, CoverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return CoverName;
        }
    }
}
