namespace BankruptcyLaw.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class JudgesController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public JudgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/Judges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Judges.ToListAsync());
        }

        // GET: Administration/Judges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (judge == null)
            {
                return NotFound();
            }

            return View(judge);
        }

        // GET: Administration/Judges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Judges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email,CourtRoom,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Judge judge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(judge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(judge);
        }

        // GET: Administration/Judges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges.FindAsync(id);
            if (judge == null)
            {
                return NotFound();
            }
            return View(judge);
        }

        // POST: Administration/Judges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Phone,Email,CourtRoom,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Judge judge)
        {
            if (id != judge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(judge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JudgeExists(judge.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(judge);
        }

        // GET: Administration/Judges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (judge == null)
            {
                return NotFound();
            }

            return View(judge);
        }

        // POST: Administration/Judges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var judge = await _context.Judges.FindAsync(id);
            _context.Judges.Remove(judge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JudgeExists(int id)
        {
            return _context.Judges.Any(e => e.Id == id);
        }
    }
}
