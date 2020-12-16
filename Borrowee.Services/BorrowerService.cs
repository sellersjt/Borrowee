using Borrowee.Data;
using Borrowee.Data.Entities;
using Borrowee.Models.BorrowerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public class BorrowerService
    {
        private readonly Guid _userId;

        public BorrowerService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateBorrower(BorrowerCreate model)
        {
            var entity =
                new Item()
                {
                    OwnerId = _userId
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Borrowers.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<BorrowerListItem>> GetBorrowers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     ctx
                        .Borrowers
                        .Where(b => b.OwnerId == _userId)
                        .Select(
                            b =>
                                new ItemListItem
                                {
                                    Id = b.Id,
                                    FirstName = b.FirstName,
                                    LastName = b.LastName,
                                    PhoneNumber = b.PhoneNumber,
                                    EmailAddress = b.EmailAddress
                                });

                return await query.ToArrayAsync();
            }
        }

        public async Task<BorrowerDetail> GetBorrowerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Borrowers
                    .SingleAsync(b => b.Id == id && b.OwnerId == _userId);

                return
                    new BorrowerDetail
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        PhoneNumber = entity.PhoneNumber,
                        EmailAddress = entity.EmailAddress
                    };
            }
        }

        public async Task<bool> UpdateBorrower(BorrowerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Borrowers
                    .SingleAsync(b => b.Id == model.Id && b.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.PhoneNumber = model.PhoneNumber;
                entity.EmailAddress = model.EmailAddress;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteBorrower(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Borrowers
                    .SingleAsync(b => b.Id == id && b.OwnerId == _userId);

                ctx.Items.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}