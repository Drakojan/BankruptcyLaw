namespace BankruptcyLaw.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class TrusteesController : AdministrationController
    {
        private readonly ITrusteesService trusteesService;

        public TrusteesController(ITrusteesService trusteesService)
        {
            this.trusteesService = trusteesService;
        }

        // GET: Administration/Trustees
        public async Task<IActionResult> Index()
        {
            return this.View(await this.trusteesService.GetAllTrusteesAsync());
        }

        // GET: Administration/Trustees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var trustee = await this.trusteesService.GetTrusteeByIdAsync(id);

            if (trustee == null)
            {
                return this.NotFound();
            }

            return this.View(trustee);
        }

        // GET: Administration/Trustees/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Trustees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Address,Phone,Email,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Trustee trustee)
        {
            if (this.ModelState.IsValid)
            {
                await this.trusteesService.AddTrusteeAsync(trustee);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(trustee);
        }

        // GET: Administration/Trustees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var trustee = await this.trusteesService.GetTrusteeByIdAsync(id);
            if (trustee == null)
            {
                return this.NotFound();
            }

            return this.View(trustee);
        }

        // POST: Administration/Trustees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Address,Phone,Email,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Trustee trustee)
        {
            if (id != trustee.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.trusteesService.EditTrustee(trustee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await this.TrusteeExists(trustee.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(trustee);
        }

        // GET: Administration/Trustees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var trustee = await this.trusteesService.GetTrusteeByIdAsync(id);

            if (trustee == null)
            {
                return this.NotFound();
            }

            return this.View(trustee);
        }

        // POST: Administration/Trustees/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trustee = await this.trusteesService.GetTrusteeByIdAsync(id);
            await this.trusteesService.HardDeleteTrustee(trustee);

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<bool> TrusteeExists(int id)
        {
            var result = await this.trusteesService.GetTrusteeByIdAsync(id);
            if (result is null)
            {
                return false;
            }

            return true;
        }
    }
}
