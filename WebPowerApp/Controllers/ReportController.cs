using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPowerApp.Context;
using WebPowerApp.Models;

namespace WebPowerApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ReportController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: EnumValues
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports.ToListAsync());
        }

        // GET: EnumValues/Create
        //[Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ReportModel());
            else
                return View(_context.Reports.Find(id));
        }

        // POST: EnumValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(ReportModel dataObj)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (dataObj.Id == 0)
                    _context.Add(dataObj);
                else
                    _context.Update(dataObj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataObj);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var findDataObj = await _context.Reports.FindAsync(id);
            if (findDataObj != null)
                _context.Reports.Remove(findDataObj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
