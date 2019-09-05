using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ServerlessTODOList.Common;
using ServerlessTODOList.DataAccess;

namespace ServerlessTODOList.Frontend.Pages
{
    public class EditListModel : PageModel
    {
        ITODOListDataAccess DataAccess { get; set; }

        public TODOList TODOList { get; set; }

        public EditListModel(ITODOListDataAccess dataAccess)
        {
            this.DataAccess = dataAccess;
        }

        public async Task OnGet(string id)
        {
            this.TODOList = await this.DataAccess.GetTODOListAsync(this.User.Identity.Name, id);
        }

        public async Task<IActionResult> OnPost(string listId, string tasks)
        {
            this.TODOList = await this.DataAccess.GetTODOListAsync(this.User.Identity.Name, listId);
            this.TODOList.Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TODOListItem>>(tasks);
            await this.DataAccess.SaveTODOListAsync(this.TODOList);

            return RedirectToPage("MyLists");
        }
    }
}