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
    public class EditModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IMessageService messageService, ILogger<EditModel> logger)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // admin inputs is valid
                if (await _messageService.UpdateContactAsync(Contact) != null)
                {
                    TempData["Success"] = "Message successfully Update.";

                    return RedirectToPage("Index");
                }
                else
                {
                    // db failure
                    TempData["Message"] = "database error, contact the developer to fix the error.";

                    _logger.LogError($"Message {nameof(EditModel)} database error.");

                    return Page();
                }
            }

            // invalid inputs
            TempData["Message"] = "Your inputs is not valid.";

            _logger.LogError($"Message {nameof(EditModel)} invalid input.");

            return Page();
        }


    }
}
