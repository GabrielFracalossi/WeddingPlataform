using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlatform.Data;
using WeddingPlatform.Models;
using WeddingPlatform.ViewModels;

namespace WeddingPlatform.Controllers
{
    [Authorize]
    public class WeddingSiteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
    
        public WeddingSiteController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var sites = await _context.WeddingSites
                .Include(w => w.Template)
                .Where(w => w.UserId == user.Id)
                .ToListAsync();
            return View(sites);
        }
    
        public IActionResult Create()
        {
            var templates = _context.Templates.Where(t => t.IsActive).ToList();
            var viewModel = new CreateWeddingSiteViewModel
            {
                Templates = templates
            };
            return View(viewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(CreateWeddingSiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var weddingSite = new WeddingSite
                {
                    Title = model.Title,
                    Url = GenerateUniqueUrl(model.Title),
                    UserId = user.Id,
                    TemplateId = model.TemplateId,
                    WeddingDate = model.WeddingDate,
                    CreatedAt = DateTime.UtcNow
                };
    
                _context.WeddingSites.Add(weddingSite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = weddingSite.Id });
            }
    
            model.Templates = _context.Templates.Where(t => t.IsActive).ToList();
            return View(model);
        }
    
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var weddingSite = await _context.WeddingSites
                .Include(w => w.Template)
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == user.Id);
    
            if (weddingSite == null)
            {
                return NotFound();
            }
    
            var viewModel = new EditWeddingSiteViewModel
            {
                Id = weddingSite.Id,
                Title = weddingSite.Title,
                WeddingDate = weddingSite.WeddingDate,
                CustomCSS = weddingSite.CustomCSS,
                CustomHTML = weddingSite.CustomHTML,
                TemplateId = weddingSite.TemplateId ?? 0,
                Templates = _context.Templates.Where(t => t.IsActive).ToList()
            };
    
            return View(viewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult> Edit(EditWeddingSiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var weddingSite = await _context.WeddingSites
                    .FirstOrDefaultAsync(w => w.Id == model.Id && w.UserId == user.Id);
    
                if (weddingSite == null)
                {
                    return NotFound();
                }
    
                weddingSite.Title = model.Title;
                weddingSite.WeddingDate = model.WeddingDate;
                weddingSite.CustomCSS = model.CustomCSS;
                weddingSite.CustomHTML = model.CustomHTML;
                weddingSite.TemplateId = model.TemplateId;
    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
    
            model.Templates = _context.Templates.Where(t => t.IsActive).ToList();
            return View(model);
        }
    
        private string GenerateUniqueUrl(string title)
        {
            var urlBase = title.ToLower().Replace(" ", "-");
            var url = urlBase;
            var counter = 1;
    
            while (_context.WeddingSites.Any(w => w.Url == url))
            {
                url = $"{urlBase}-{counter}";
                counter++;
            }
    
            return url;
        }
    }
}