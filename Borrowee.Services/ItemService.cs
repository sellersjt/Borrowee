using Borrowee.Data;
using Borrowee.Data.Entities;
using Borrowee.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public class ItemService
    {
        private readonly Guid _userId;

        public ItemService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateItem(ItemCreate model)
        {
            var entity =
                new Item()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Description = model.Description,
                    ModelNumber = model.ModelNumber,
                    SerialNumber = model.SerialNumber,
                    Value = model.Value,
                    ItemImageId = model.ItemImageId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ItemListItem>> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .Items
                        .Where(i => i.OwnerId == _userId)
                        .Select(
                            i =>
                                new ItemListItem
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Description = i.Description,
                                    ModelNumber = i.ModelNumber,
                                    SerialNumber = i.SerialNumber,
                                    Value = i.Value,
                                    ItemImage = i.ItemImage
                                });

                return await query.ToArrayAsync();
            }
        }

        public async Task<ItemDetail> GetItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Items
                    .SingleAsync(i => i.Id == id && i.OwnerId == _userId);

                return
                    new ItemDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        ModelNumber = entity.ModelNumber,
                        SerialNumber = entity.SerialNumber,
                        Value = entity.Value,
                        ItemImageId = entity.ItemImageId,
                        ItemImage = entity.ItemImage
                    };
            }
        }

        public async Task<bool> UpdateItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Items
                    .SingleAsync(i => i.Id == model.Id && i.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.ModelNumber = model.ModelNumber;
                entity.SerialNumber = model.SerialNumber;
                entity.Value = model.Value;
                entity.ItemImageId = model.ItemImageId;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Items
                    .SingleAsync(i => i.Id == id && i.OwnerId == _userId);

                ctx.Items.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ItemListItem>> GetItemsByImageId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .Items
                        .Where(i => i.ItemImageId == id && i.OwnerId == _userId)
                        .Select(
                            i =>
                                new ItemListItem
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Description = i.Description,
                                    ModelNumber = i.ModelNumber,
                                    SerialNumber = i.SerialNumber,
                                    Value = i.Value,
                                    ItemImage = i.ItemImage
                                });

                return await query.ToArrayAsync();
            }
        }
    }
}
