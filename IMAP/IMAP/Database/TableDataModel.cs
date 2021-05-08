using System.Collections.Generic;
using System.Threading.Tasks;
using IMAP.Database;

namespace IMAP.Model
{
    class TableDataModel<T> where T : new()
    {
        public async Task CreateTable()
        {
            await Data.Database.CreateTable<T>();
        }

        public async Task<List<T>> GetItems()
        {
            return await Data.Database.GetItemsAsync<T>();
        }
    }
}
