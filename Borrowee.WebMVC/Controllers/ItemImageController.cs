using Borrowee.Models.ItemImageModels;
using Borrowee.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Borrowee.WebMVC.Controllers
{
    [Authorize]
    public class ItemImageController : Controller
    {
        // GET: ItemImages
        public async Task<ActionResult> Index()
        {
            var service = CreateItemImageService();
            var model = await service.GetItemImages();

            return View(model);
        }

        // GET: ItemImages/Upload
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            //check the user has entered a file
            if (file != null)
            {
                //check if the file is valid
                if (ValidateFile(file))
                {
                    try
                    {
                        SaveFileToDisk(file);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "Sorry an error occurred saving the file to disk, please try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg and less than 2MB in size");
                }
            }
            else
            {
                //if the user has not entered a file return an error message
                ModelState.AddModelError("FileName", "Please choose a file");
            }

            if (ModelState.IsValid)
            {
                var model = new ItemImageCreate
                {
                    FileName = file.FileName
                };

                var service = CreateItemImageService();

                string responce = await service.CreateItemImage(model);

                if (responce == "success")
                {
                    TempData["SaveResult"] = "Your file was uploaded.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("FileName", responce);
            }

            return View();
        }

        // GET: Details
        // ItemImage/Details/{id}
        public async Task<ActionResult> Details(int id)
        {
            var service = CreateItemImageService();
            var model = await service.GetItemImageById(id);

            return View(model);
        }

        // GET: Delete
        // ItemImage/Delete/{id}
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateItemImageService();
            var model = await service.GetItemImageById(id);

            return View(model);
        }

        // POST: Delete
        // ItemImage/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItemImage(int id, ItemImageDetail model)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateItemImageService();

            await service.DeleteItemImage(id);

            TempData["SaveResult"] = "Your item image was deleted.";

            return RedirectToAction("Index");
        }

        private ItemImageService CreateItemImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemImageService(userId);
            return service;
        }

        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) &&
            allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constants.ItemImagePath + file.FileName);
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constants.ItemThumbnailPath + file.FileName);
        }
    }
}