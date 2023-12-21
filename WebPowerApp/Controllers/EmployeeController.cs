using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerBI.Api.Models;
using WebPowerApp.Context;
using WebPowerApp.Models;

namespace WebPowerApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EmployeeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: EnumValues
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees
                .Include(e => e.RoleModel)
                .ToListAsync());
        }

        // GET: EnumValues/Create
        //[Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            var roleList = (from r in _context.Roles.Where(p => p.IsActive == true )
                                     select new SelectListItem()
                                     {
                                         Value = r.Id.ToString(),
                                         Text = r.RoleName.ToString()
                                     }).ToList();

            ViewBag.RoleList = roleList;


            if (id == 0)
                return View(new EmployeeModel());
            else
                return View(_context.Employees.Find(id));
        }

        // POST: EnumValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(EmployeeModel employeeModel)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (employeeModel.Id == 0)
                    _context.Add(employeeModel);
                else
                    _context.Update(employeeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var Obj = await _context.Employees.FindAsync(id);
            if (Obj != null)
                _context.Employees.Remove(Obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
