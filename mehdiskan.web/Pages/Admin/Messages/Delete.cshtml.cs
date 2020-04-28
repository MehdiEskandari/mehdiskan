using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mehdiskan.web.Pages.Admin.Messages
{
    public class DeleteModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IMessageService messageService, ILogger<DeleteModel> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }
        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Contact = await _messageService.GetContactByIdAsync(id.Value);

            if (Contact == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            TempData["Success"] = "Message successfully removed.";
            await _messageService.RemoveContactAsync(id);

            return RedirectToPage("Index");
        }
    }
}
