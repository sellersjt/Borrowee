using Borrowee.Data.Entities;
using Borrowee.Models.ItemModels;
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
    public class ItemController : Controller
    {
        // GET: Items
        public async Task<ActionResult> Index()
        {
            var itemService = CreateItemService();
            var model = await itemService.GetItems();

            return View(model);
        }

        // GET: Create
        public async Task<ActionResult> Create()
        {
            var itemImageService = CreateItemImageService();
            var images = await itemImageService.GetItemImages();

            var viewModel = new CreateItemViewModel();

            viewModel.Images = images.OrderBy(n => n.FileName).Select(i => new SelectListItem
            {
                Text = i.FileName,
                Value = i.Id.ToString()
            });

            return View(viewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var itemService = CreateItemService();

            var itemModel = new ItemCreate
            {
                Name = model.Name,
                Description = model.Description,
                ModelNumber = model.ModelNumber,
                SerialNumber = model.SerialNumber,
                Value = model.Value,
                ItemImageId = model.ItemImageId
            };

            if (await itemService.CreateItem(itemModel))
            {
                TempData["SaveResult"] = "Your item was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Item could not be created");

            var itemImageService = CreateItemImageService();
            var itemImages = await itemImageService.GetItemImages();

            model.Images = itemImages.OrderBy(f => f.FileName).Select(i => new SelectListItem
            {
                Text = i.FileName,
                Value = i.Id.ToString()
            });

            return View(model);
        }

        // GET: Details
        // Item/Details/{id}
        public async Task<ActionResult> Details(int id)
        {
            var itemService = CreateItemService();
            var model = await itemService.GetItemById(id);

            return View(model);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int id)
        {
            var itemService = CreateItemService();
            var detail = await itemService.GetItemById(id);

            var itemImageService = CreateItemImageService();
            var images = await itemImageService.GetItemImages();

            var viewModel =
                new EditItemViewModel
                {
                    Id = detail.Id,
                    Name = detail.Name,
                    Description = detail.Description,
                    ModelNumber = detail.ModelNumber,
                    SerialNumber = detail.SerialNumber,
                    Value = detail.Value,
                    ItemImageId = detail.ItemImageId
                };

            viewModel.Images = images.OrderBy(n => n.FileName).Select(i => new SelectListItem
            {
                Text = i.FileName,
                Value = i.Id.ToString()
            });

            return View(viewModel);
        }

        // POST: EDIT
        // Item/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ItemEdit model)
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

            var itemService = CreateItemService();

            if (await itemService.UpdateItem(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        // GET: Delete
        // Item/Delete/{id}
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemService = CreateItemService();
            var model = await itemService.GetItemById(id);

            return View(model);
        }

        // POST: Delete
        // Item/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItem(int id, ItemDetail model)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var itemService = CreateItemService();

            await itemService.DeleteItem(id);

            TempData["SaveResult"] = "Your item was deleted.";

            return RedirectToAction("Index");
        }

        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            return service;
        }

        private ItemImageService CreateItemImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemImageService(userId);
            return service;
        }
    }
}