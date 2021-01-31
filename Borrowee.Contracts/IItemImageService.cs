using Borrowee.Models.ItemImageModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borrowee.Services
{
    public interface IItemImageService
    {
        Task<string> CreateItemImage(ItemImageCreate model);
        Task<bool> DeleteItemImage(int id);
        Task<ItemImageDetail> GetItemImageById(int id);
        Task<IEnumerable<ItemImageListItem>> GetItemImages();
    }
}