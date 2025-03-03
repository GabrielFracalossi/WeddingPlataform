using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlatform.Data;
using WeddingPlatform.Models;

namespace WeddingPlatform.Controllers
{
    [Authorize]
    public class GiftRegistryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiftRegistryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gifts = await _context.GiftRegistry.ToListAsync();
            return View(gifts);
        }
    }
}