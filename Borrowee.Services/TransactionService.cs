using Borrowee.Data;
using Borrowee.Data.Entities;
using Borrowee.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    OwnerId = _userId,
                    ItemId = model.ItemId,
                    BorrowerId = model.BorrowerId,
                    LentOutDateUtc = model.LentOutDateUtc,
                    ReturnDateUtc = model.ReturnDateUtc,
                    IsReturned = model.IsReturned
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<TransactionListItem>> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .Transactions
                        .Where(t => t.OwnerId == _userId)
                        .Select(
                            t =>
                                new TransactionListItem
                                {
                                    Id = t.Id,
                                    Item = t.Item,
                                    Borrower = t.Borrower,
                                    LentOutDateUtc = t.LentOutDateUtc,
                                    ReturnDateUtc = t.ReturnDateUtc,
                                    IsReturned = t.IsReturned,
                                    ItemImage = ctx.ItemImages.Where(i => i.Id == t.Item.ItemImageId && t.OwnerId == _userId).FirstOrDefault()
                                });

                return await query.ToArrayAsync();
            }
        }

        public async Task<TransactionDetail> GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Transactions
                    .SingleAsync(t => t.Id == id && t.OwnerId == _userId);

                return
                    new TransactionDetail
                    {
                        Id = entity.Id,
                        Item = entity.Item,
                        Borrower = entity.Borrower,
                        LentOutDateUtc = entity.LentOutDateUtc,
                        ReturnDateUtc = entity.ReturnDateUtc,
                        IsReturned = entity.IsReturned,
                        ItemImage = ctx.ItemImages.Where(i => i.Id == entity.Item.ItemImageId && i.OwnerId == _userId).FirstOrDefault()
                    };
            }
        }

        public async Task<bool> UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Transactions
                    .SingleAsync(t => t.Id == model.Id && t.OwnerId == _userId);

                entity.ItemId = model.ItemId;
                entity.BorrowerId = model.BorrowerId;
                entity.LentOutDateUtc = model.LentOutDateUtc;
                entity.ReturnDateUtc = model.ReturnDateUtc;
                entity.IsReturned = model.IsReturned;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Transactions
                    .SingleAsync(t => t.Id == id && t.OwnerId == _userId);

                ctx.Transactions.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<TransactionListItem>> GetItemsOnLoan()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .Transactions
                        .Where(t => t.IsReturned == false && t.OwnerId == _userId)
                        .Select(
                            t =>
                                new TransactionListItem
                                {
                                    Id = t.Id,
                                    Item = t.Item,
                                    Borrower = t.Borrower,
                                    LentOutDateUtc = t.LentOutDateUtc,
                                    ReturnDateUtc = t.ReturnDateUtc,
                                    IsReturned = t.IsReturned,
                                    ItemImage = ctx.ItemImages.Where(i => i.Id == t.Item.ItemImageId && t.OwnerId == _userId).FirstOrDefault()
                                });

                return await query.ToArrayAsync();
            }
        }
    }
}
