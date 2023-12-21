using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerBI.Api.Models;
using WebPowerApp.Context;
using WebPowerApp.Models;

namespace WebPowerApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RoleController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: EnumValues
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: EnumValues/Create
        //[Authorize]
        public IActionResult AddOrEdit(int id = 0)
        { if (id == 0)
                return View(new RoleModel());
            else
                return View(_context.Roles.Find(id));
        }

        // POST: EnumValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(RoleModel roleModel)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (roleModel.Id == 0)
                    _context.Add(roleModel);
                else
                    _context.Update(roleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleModel);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var roleObj = await _context.Roles.FindAsync(id);
            if(roleObj!=null)
                _context.Roles.Remove(roleObj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
