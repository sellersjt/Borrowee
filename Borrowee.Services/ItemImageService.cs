using Borrowee.Data;
using Borrowee.Data.Entities;
using Borrowee.Models.ItemImageModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public class ItemImageService : IItemImageService
    {
        private readonly Guid _userId;

        public ItemImageService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<string> CreateItemImage(ItemImageCreate model)
        {
            var entity =
                new ItemImage()
                {
                    OwnerId = _userId,
                    FileName = model.FileName
                };

            using (var ctx = new ApplicationDbContext())
            {
                string message = "";
                ctx.ItemImages.Add(entity);
                try
                {
                    if (await ctx.SaveChangesAsync() == 1)
                    {
                        message = "success";
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException.InnerException as SqlException;
                    if (innerException != null && innerException.Number == 2601)
                    {
                        message = "The file " + model.FileName +
                            " already exists in the system. Please delete it and try again if you wish to re-add it";
                    }
                    else
                    {
                        message = "Sorry an error has occurred saving to the database, please try again";
                    }
                }
                return message;
            }
        }

        public async Task<IEnumerable<ItemImageListItem>> GetItemImages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .ItemImages
                        .Where(i => i.OwnerId == _userId)
                        .Select(
                            i =>
                                new ItemImageListItem
                                {
                                    Id = i.Id,
                                    FileName = i.FileName
                                });

                return await query.ToArrayAsync();
            }
        }

        public async Task<ItemImageDetail> GetItemImageById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .ItemImages
                    .SingleAsync(i => i.Id == id && i.OwnerId == _userId);

                return
                    new ItemImageDetail
                    {
                        Id = entity.Id,
                        FileName = entity.FileName
                    };
            }
        }

        public async Task<bool> DeleteItemImage(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .ItemImages
                    .SingleAsync(i => i.Id == id && i.OwnerId == _userId);

                ctx.ItemImages.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
