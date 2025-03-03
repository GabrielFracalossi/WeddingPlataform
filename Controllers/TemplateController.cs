using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlatform.Data;
using WeddingPlatform.Models;
using WeddingPlatform.ViewModels;

namespace WeddingPlatform.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var templates = await _context.Templates.Where(t => t.IsActive).ToListAsync();
            return View(templates);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var template = new Template
                {
                    Name = model.Name,
                    Description = model.Description,
                    HTML = model.HTML,
                    CSS = model.CSS,
                    IsActive = model.IsActive
                };

                _context.Templates.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            var viewModel = new TemplateViewModel
            {
                Id = template.Id,
                Name = template.Name,
                Description = template.Description,
                HTML = template.HTML,
                CSS = template.CSS,
                IsActive = template.IsActive
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TemplateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var template = await _context.Templates.FindAsync(id);
                if (template == null)
                {
                    return NotFound();
                }

                template.Name = model.Name;
                template.Description = model.Description;
                template.HTML = model.HTML;
                template.CSS = model.CSS;
                template.IsActive = model.IsActive;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}