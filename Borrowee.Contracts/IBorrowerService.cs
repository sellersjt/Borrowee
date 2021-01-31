using Borrowee.Models.BorrowerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public interface IBorrowerService
    {
        Task<bool> CreateBorrower(BorrowerCreate model);
        Task<bool> DeleteBorrower(int id);
        Task<BorrowerDetail> GetBorrowerById(int id);
        Task<IEnumerable<BorrowerListItem>> GetBorrowers();
        Task<bool> UpdateBorrower(BorrowerEdit model);
    }
}