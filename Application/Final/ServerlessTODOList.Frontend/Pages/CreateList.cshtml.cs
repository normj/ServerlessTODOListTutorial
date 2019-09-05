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
    public class CreateListModel : PageModel
    {
        ITODOListDataAccess DataAccess { get; set; }

        [BindProperty]
        public string Name { get; set; }


        public CreateListModel(ITODOListDataAccess dataAccess)
        {
            this.DataAccess = dataAccess;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var list = new TODOList
            {
                User = this.User.Identity.Name,
                Name = this.Name
            };

            await this.DataAccess.SaveTODOListAsync(list);
            return RedirectToPage("EditList", new { Id = list.ListId });
        }
    }
}