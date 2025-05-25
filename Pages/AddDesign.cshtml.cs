using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FabricFrame.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


public class AddDesignModel : PageModel
{
    private readonly StoreDBContext _context;
    private readonly IWebHostEnvironment _environment;

    public AddDesignModel(StoreDBContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> OnPostAsync(IFormFile Image, string DesignName, string Description, decimal Price)
    {
        if (Image == null || Image.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return Page();
        }

        var fileName = Path.GetFileName(Image.FileName);
        var savePath = Path.Combine(_environment.WebRootPath, "images", fileName);

        using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await Image.CopyToAsync(stream);
        }

        var design = new Design
        {
            DesignName = DesignName,
            Description = Description,
            Price = Price,
            ImageUrl = "/images/" + fileName
        };

        _context.Designs.Add(design);
        await _context.SaveChangesAsync();

        return RedirectToPage("Gallery");
    }
}
