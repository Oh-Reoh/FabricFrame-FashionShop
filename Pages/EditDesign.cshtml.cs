using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FabricFrame.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

public class EditDesignModel : PageModel
{
    private readonly StoreDBContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditDesignModel(StoreDBContext context, IWebHostEnvironment environment)
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

    public async Task<IActionResult> OnPostAsync(IFormFile Image)
    {
        var designToUpdate = _context.Designs.FirstOrDefault(d => d.DesignId == Design.DesignId);
        if (designToUpdate == null) return RedirectToPage("Gallery");

        designToUpdate.DesignName = Design.DesignName;
        designToUpdate.Description = Design.Description;
        designToUpdate.Price = Design.Price;

        if (Image != null && Image.Length > 0)
        {
            var fileName = Path.GetFileName(Image.FileName);
            var savePath = Path.Combine(_environment.WebRootPath, "images", fileName);
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            designToUpdate.ImageUrl = "/images/" + fileName;
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("Gallery");
    }
}
