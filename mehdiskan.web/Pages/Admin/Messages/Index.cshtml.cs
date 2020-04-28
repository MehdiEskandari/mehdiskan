using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mehdiskan.web.Interfaces;
using mehdiskan.web.ViewModels;

namespace mehdiskan.web.Pages.Admin.Messages
{
    public class IndexModel : PageModel
    {
        private readonly IMessageService _messageService;

        public IndexModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public ContactPagingViewModel ContactPagingVM { get; private set; }
        public int ContactsCount { get; private set; }


        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 24)
        {
            ContactPagingVM = await _messageService.GetContactsAsync(pageNumber, pageSize);
            ContactsCount = await _messageService.ContactsCountAsync();
        }
    }
}
