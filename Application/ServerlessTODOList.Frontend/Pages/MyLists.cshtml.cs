using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ServerlessTODOList.Common;
using ServerlessTODOList.DataAccess;

namespace ServerlessTODOList.Frontend.Pages
{
    [Authorize]
    public class MyListsModel : PageModel
    {
        ITODOListDataAccess DataAccess { get; set; }

        public IList<TODOList> TODOLists { get; set; }

        public MyListsModel(ITODOListDataAccess dataAccess)
        {
            this.DataAccess = dataAccess;
        }

        public async Task OnGet()
        {
            this.TODOLists = await this.DataAccess.GetTODOListsForUserAsync(this.User.Identity.Name);
        }


        public async Task OnPostDeleteAsync(string listIdToDelete)
        {
            await this.DataAccess.DeleteTODOList(this.User.Identity.Name, listIdToDelete);
            this.TODOLists = await this.DataAccess.GetTODOListsForUserAsync(this.User.Identity.Name);
        }


        public static string FormattedCompleteCount(TODOList list)
        {
            if (list.Items == null)
                return "0/0";

            return list.Items.Where(x => x.Complete).Count() + "/" + list.Items.Count;
        }
    }
}