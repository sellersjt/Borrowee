using Borrowee.Models.TransactionModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public interface ITransactionService
    {
        Task<bool> CreateTransaction(TransactionCreate model);
        Task<bool> DeleteTransaction(int id);
        Task<IEnumerable<TransactionListItem>> GetItemsOnLoan();
        Task<TransactionDetail> GetTransactionById(int id);
        Task<IEnumerable<TransactionListItem>> GetTransactions();
        Task<bool> UpdateTransaction(TransactionEdit model);
    }
}