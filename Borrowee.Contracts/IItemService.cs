using Borrowee.Models.ItemModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public interface IItemService
    {
        Task<bool> CreateItem(ItemCreate model);
        Task<bool> DeleteItem(int id);
        Task<ItemDetail> GetItemById(int id);
        Task<IEnumerable<ItemListItem>> GetItems();
        Task<IEnumerable<ItemListItem>> GetItemsByImageId(int id);
        Task<bool> UpdateItem(ItemEdit model);
    }
}