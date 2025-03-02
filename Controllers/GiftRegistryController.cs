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
    public class GiftRegistryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
    
        public GiftRegistryController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> Index(int weddingSiteId)
        {
            var user = await _userManager.GetUserAsync(User);
            var weddingSite = await _context.WeddingSites
                .Include(w => w.GiftRegistry)
                .FirstOrDefaultAsync(w => w.Id == weddingSiteId && w.UserId == user.Id);
    
            if (weddingSite == null)
            {
                return NotFound();
            }
    
            return View(weddingSite.GiftRegistry);
        }
    
        public IActionResult Create(int weddingSiteId)
        {
            var viewModel = new GiftRegistryViewModel
            {
                WeddingSiteId = weddingSiteId
            };
            return View(viewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(GiftRegistryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var weddingSite = await _context.WeddingSites
                    .FirstOrDefaultAsync(w => w.Id == model.WeddingSiteId && w.UserId == user.Id);
    
                if (weddingSite == null)
                {
                    return NotFound();
                }
    
                var gift = new GiftRegistry
                {
                    WeddingSiteId = model.WeddingSiteId,
                    ItemName = model.ItemName,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl
                };
    
                _context.GiftRegistry.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingSiteId = model.WeddingSiteId });
            }
    
            return View(model);
        }
    
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Purchase(int id, string purchaserName)
        {
            var gift = await _context.GiftRegistry.FindAsync(id);
            if (gift == null || gift.IsPurchased)
            {
                return Json(new { success = false });
            }
    
            gift.IsPurchased = true;
            gift.PurchasedBy = purchaserName;
            await _context.SaveChangesAsync();
    
            return Json(new { success = true });
        }
    }
}