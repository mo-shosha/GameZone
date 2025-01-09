using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
        private readonly ICategoriesService _categoryService;
        private readonly IDevicesService _DeviceService;
        private readonly IGamesService _GamesService;
        public GamesController(ICategoriesService categoryService, IDevicesService DeviceService, IGamesService GamesService)
		{
			_DeviceService = DeviceService;
            _categoryService = categoryService;
			_GamesService = GamesService;
        }
        public async Task<IActionResult> Index()
        {
            var games = await _GamesService.GetAll();
            return View(games);  
        }

        public IActionResult Details(int? id)
        {
            var game=_GamesService.Get(id);
            if (game != null)
            {
                return View(game);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoryService.GetSelectList(),
                Devices = _DeviceService.GetSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectList();
                model.Devices = _DeviceService.GetSelectList();
                return View(model);
            }

            await _GamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _GamesService.Get(id);
            if (game == null)
            {
                return NotFound();
            }
            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoryService.GetSelectList(),
                Devices = _DeviceService.GetSelectList(),
                CurrentCover = game.Cover
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetSelectList();
                model.Devices = _DeviceService.GetSelectList();
                return View(model);
            }

            var game = await _GamesService.Update(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var isDeleted = _GamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

    }
}
