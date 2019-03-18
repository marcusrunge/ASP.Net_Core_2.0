using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CRUDWebApplication.Pages
{
    public class CreateModel : PageModel
    {
        private ILogger<CreateModel> _logger;
        private readonly AppDbContext _appDbContext;
        [BindProperty]
        public Customer Customer { get; set; }
        [TempData]
        public string Message { get; set; }
        public CreateModel(AppDbContext appDbContext, ILogger<CreateModel> logger)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _appDbContext.Customers.Add(Customer);
            await _appDbContext.SaveChangesAsync();
            Message = $"Customer {Customer.Name} added";
            _logger.LogCritical(Message);
            return RedirectToPage("/Index");
        }
    }
}