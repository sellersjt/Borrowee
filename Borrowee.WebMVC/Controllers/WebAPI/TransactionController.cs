using Borrowee.Models.TransactionModels;
using Borrowee.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Borrowee.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Transaction")]
    public class TransactionController : ApiController
    {
        [Route("{id}/Return")]
        [HttpPut]
        public async Task<bool> ToggleReturnd(int id) => await SetReturned(id, true);

        [Route("{id}/Return")]
        [HttpDelete]
        public async Task<bool> ToggleNotReturnd(int id) => await SetReturned(id, false);

        private async Task<bool> SetReturned(int id, bool isReturned)
        {
            DateTimeOffset? returnedDate = null;
            if (isReturned)
            {
                returnedDate = DateTimeOffset.Now;
            }

            var transactionService = CreateTransactionService();
            var detail = await transactionService.GetTransactionById(id);

            var updatedTransaction =
                new TransactionEdit
                {
                    Id = detail.Id,
                    ItemId = detail.Item.Id,
                    BorrowerId = detail.Borrower.Id,
                    LentOutDateUtc = detail.LentOutDateUtc,
                    ReturnDateUtc = returnedDate,
                    IsReturned = isReturned
                };

            return await transactionService.UpdateTransaction(updatedTransaction);
        }

        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransactionService(userId);
            return service;
        }
    }
}
