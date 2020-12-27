using Borrowee.Models.TransactionModels;
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
    public class TransactionController : Controller
    {
        // GET: Transactions
        public async Task<ActionResult> Index()
        {
            var transactionService = CreateTransactionService();
            var model = await transactionService.GetTransactions();

            return View(model);
        }

        // GET: Create
        public async Task<ActionResult> Create()
        {
            var itemService = CreateItemService();
            var items = await itemService.GetItems();

            var borrowerService = CreateBorrowerService();
            var borrowers = await borrowerService.GetBorrowers();

            var viewModel = new CreateTransactionViewModel();

            viewModel.Items = items.OrderBy(n => n.Name).Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            viewModel.Borrowers = borrowers.OrderBy(n => n.FirstName).Select(b => new SelectListItem
            {
                Text = b.FirstName + " " + b.LastName,
                Value = b.Id.ToString()
            });

            viewModel.LentOutDateUtc = DateTime.Now;

            return View(viewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransactionCreate model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var transactionService = CreateTransactionService();

            if (await transactionService.CreateTransaction(model))
            {
                TempData["SaveResult"] = "Your transaction was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Transaction could not be created");

            return View(model);
        }

        // GET: Details
        // Transaction/Details/{id}
        public async Task<ActionResult> Details(int id)
        {
            var transactionService = CreateTransactionService();
            var model = await transactionService.GetTransactionById(id);

            return View(model);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int id)
        {
            var transactionService = CreateTransactionService();
            var detail = await transactionService.GetTransactionById(id);

            var itemService = CreateItemService();
            var items = await itemService.GetItems();

            var borrowerService = CreateBorrowerService();
            var borrowers = await borrowerService.GetBorrowers();

            var viewModel =
                new EditTransactionViewModel
                {
                    Id = detail.Id,
                    ItemId = detail.Item.Id,
                    BorrowerId = detail.Borrower.Id,
                    LentOutDateUtc = detail.LentOutDateUtc,
                    ReturnDateUtc = detail.ReturnDateUtc,
                    IsReturned = detail.IsReturned
                };

            viewModel.Items = items.OrderBy(n => n.Name).Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            viewModel.Borrowers = borrowers.OrderBy(n => n.FirstName).Select(b => new SelectListItem
            {
                Text = b.FirstName + " " + b.LastName,
                Value = b.Id.ToString()
            });

            return View(viewModel);
        }

        // POST: EDIT
        // Transaction/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TransactionEdit model)
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

            var transactionService = CreateTransactionService();

            if (await transactionService.UpdateTransaction(model))
            {
                TempData["SaveResult"] = "Your transaction was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your transaction could not be updated.");
            return View(model);
        }

        // GET: Delete
        // Transaction/Delete/{id}
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var transactionService = CreateTransactionService();
            var model = await transactionService.GetTransactionById(id);

            return View(model);
        }

        // POST: Delete
        // Transaction/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItem(int id, TransactionDetail model)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var transactionService = CreateTransactionService();

            await transactionService.DeleteTransaction(id);

            TempData["SaveResult"] = "Your transaction was deleted.";

            return RedirectToAction("Index");
        }

        // GET: Items On Loan
        public async Task<ActionResult> ItemsOnLoan()
        {
            var transactionService = CreateTransactionService();
            var model = await transactionService.GetItemsOnLoan();

            return View(model);
        }

        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            return service;
        }

        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            return service;
        }

        private BorrowerService CreateBorrowerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BorrowerService(userId);
            return service;
        }
    }
}