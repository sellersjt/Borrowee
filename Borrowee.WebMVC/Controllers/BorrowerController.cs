using Borrowee.Models.BorrowerModels;
using Borrowee.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Borrowee.WebMVC.Controllers
{
    [Authorize]
    public class BorrowerController : Controller
    {
        // GET: Borrowers
        public async Task<ActionResult> Index()
        {
            var borrowerService = CreateBorrowerService();
            var model = await borrowerService.GetBorrowers();

            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View(new BorrowerCreate());
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BorrowerCreate model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var borrowerService = CreateBorrowerService();

            if (await borrowerService.CreateBorrower(model))
            {
                TempData["SaveResult"] = "Your borrower was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Borrower could not be created");

            return View(model);
        }

        // GET: Details
        // Borrower/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var borrowerService = CreateBorrowerService();
            var model = await borrowerService.GetBorrowerById((int)id);

            return View(model);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var borrowerService = CreateBorrowerService();
            var detail = await borrowerService.GetBorrowerById((int)id);

            var model =
                new BorrowerEdit
                {
                    Id = detail.Id,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    PhoneNumber = detail.PhoneNumber,
                    EmailAddress = detail.EmailAddress
                };

            return View(model);
        }

        // POST: EDIT
        // Borrower/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BorrowerEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var borrowerService = CreateBorrowerService();

            if (await borrowerService.UpdateBorrower(model))
            {
                TempData["SaveResult"] = "Your borrower was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your borrower could not be updated.");
            return View(model);
        }

        // GET: Delete
        // Borrower/Delete/{id}
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var borrowerService = CreateBorrowerService();
            var model = await borrowerService.GetBorrowerById((int)id);

            return View(model);
        }

        // POST: Delete
        // Borrower/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItem(int id, BorrowerDetail model)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var borrowerService = CreateBorrowerService();

            await borrowerService.DeleteBorrower(id);

            TempData["SaveResult"] = "Your borrower was deleted.";

            return RedirectToAction("Index");
        }

        private BorrowerService CreateBorrowerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BorrowerService(userId);
            return service;
        }
    }
}