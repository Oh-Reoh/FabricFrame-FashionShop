using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FabricFrame.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

public class DeleteDesignModel : PageModel
{
    private readonly StoreDBContext _context;
    private readonly IWebHostEnvironment _environment;

    public DeleteDesignModel(StoreDBContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public Design Design { get; set; }

    public IActionResult OnGet(int id)
    {
        Design = _context.Designs.FirstOrDefault(d => d.DesignId == id);
        if (Design == null)
        {
            return RedirectToPage("Gallery");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var design = _context.Designs.FirstOrDefault(d => d.DesignId == Design.DesignId);
        if (design == null)
        {
            return RedirectToPage("Gallery");
        }

        // Optionally remove image file from disk
        if (!string.IsNullOrEmpty(design.ImageUrl))
        {
            var imagePath = Path.Combine(_environment.WebRootPath, design.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        _context.Designs.Remove(design);
        await _context.SaveChangesAsync();

        return RedirectToPage("Gallery");
    }
}
