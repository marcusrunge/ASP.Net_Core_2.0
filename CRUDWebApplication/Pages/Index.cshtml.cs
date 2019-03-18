using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private readonly AppDbContext _appDbContext;
        [BindProperty]
        public Customer Customer { get; set; }
        public IList<Customer> Customers { get; private set; }
        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task OnGetAsync()
        {
            Customers = await _appDbContext.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customer = await _appDbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _appDbContext.Remove(customer);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
