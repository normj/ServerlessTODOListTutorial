using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ServerlessTODOList.Common;

namespace ServerlessTODOList.DataAccess
{
    public interface ITODOListDataAccess
    {
        Task<IList<TODOList>> GetTODOListsForUserAsync(string user);

        Task<TODOList> GetTODOListAsync(string user, string listId);

        Task SaveTODOListAsync(TODOList list);

        Task DeleteTODOList(string user, string listId);
    }
}
