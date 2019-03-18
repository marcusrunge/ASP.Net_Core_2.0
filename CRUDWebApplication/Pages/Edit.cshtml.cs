using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebApplication.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        [BindProperty]
        public Customer Customer { get; set; }        
        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _appDbContext.Customers.FindAsync(id);
            if (Customer == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _appDbContext.Attach(Customer).State = EntityState.Modified;
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Customer {Customer.Id} not found!", e);
            }
            return RedirectToPage("./Index");
        }
    }
}