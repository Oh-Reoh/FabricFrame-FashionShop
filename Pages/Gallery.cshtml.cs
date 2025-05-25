using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class GalleryModel : PageModel
{
    public List<string> Brands { get; set; } = new() { "offwhite", "hamu", "penshoppe", "bench", "rajo" };
    public string SelectedBrand { get; set; }

    public Dictionary<string, List<(string FileName, string ImageFile, string DisplayName, string DisplayPrice)>> ImagesByBrand { get; set; } = new();

    public void OnGet()
    {
        SelectedBrand = Request.Query["brand"].ToString().ToLower();
        if (string.IsNullOrWhiteSpace(SelectedBrand)) SelectedBrand = "all";

        string imageRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        var allPrices = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Off-White
            ["OW-Orange Shirt"] = "₱23,800",
            ["OW-Black Tee"] = "₱21,840",
            ["OW-Paint Relaxed Jeans"] = "₱55,440",
            ["OW-Crocodile Varsity Cardigan"] = "₱91,560",
            ["OW-Irregular Cargo Jeans"] = "₱47,600",
            ["OW-Cloud Arrow TShirt"] = "₱22,120",
            ["OW-Fresco Long Sleeved Top"] = "₱17,472",
            ["OW-Toybox Satin Cargo Pants"] = "₱36,064",
            ["OW-Off Stamp Ribbed Midi Dress"] = "₱35,000",
            ["OW-Lurex Tank Top"] = "₱23,800",

            // Ha.Mu
            ["HM-William Wind Breaker Jacket"] = "₱24,500",
            ["HM-Let Loose Shirt"] = "₱7,400",
            ["HM-Work Is A Party Jacket"] = "₱9,500",
            ["HM-Puff Sleeve Denim Jacket"] = "₱20,500",
            ["HM-Denim Arcade Long Shorts"] = "₱11,300",
            ["HM-Raw Edge Balloon Dress"] = "₱12,400",
            ["HM-Elemental Motif Patchwork TShirt"] = "₱5,100",
            ["HM-Hand Woven Floral Blazer"] = "₱15,100",
            ["HM-Mirror Mandarin Shirt"] = "₱31,900",
            ["HM-Hand-Knit Scarf"] = "₱42,200",

            // Penshoppe
            ["PS-JetStream Graphic TShirt"] = "₱699",
            ["PS-Slim Fit Biker Jeans"] = "₱1,399",
            ["PS-Straight Fit Cargo Pants"] = "₱1,499",
            ["PS-Crew Neck TShirt"] = "₱379",
            ["PS-Airforce Graphic TShirt"] = "₱699",
            ["PS-Two-Fer Henley Top"] = "₱499",
            ["PS-Floral TShirt"] = "₱499",
            ["PS-ClawsOn The Sticks Graphic TShirt"] = "₱379",
            ["PS-Mid Western Jeans"] = "₱1,199",
            ["PS-Bermuda Denim Shorts"] = "₱899",

            // Bench
            ["B-Milk TShirt"] = "₱499",
            ["B-Flower Tshirt"] = "₱499",
            ["B-Beach Bear Tshirt"] = "₱499",
            ["B-Mens Twill Pants"] = "₱1,299",
            ["B-Smile Beach Shorts"] = "₱899",
            ["B-Always Fresh Shirt"] = "₱459",
            ["B-Ride Waves Shirt"] = "₱439",
            ["B-Faded Denim Pants"] = "₱1,199",
            ["B-Denim Culottes Black"] = "₱999",
            ["B-Parachute Skirt"] = "₱899",

            // Rajo Laurel
            ["RL-Selen Gown"] = "₱15,500",
            ["RL-Geom Dress"] = "₱13,500",
            ["RL-Galli Dress"] = "₱10,500",
            ["RL-Kin Gown"] = "₱15,500",
            ["RL-Enoki Dress"] = "₱12,500",
            ["RL-Imani Set"] = "₱24,490",
            ["RL-Knoop Set"] = "₱18,290",
            ["RL-Opa Set"] = "₱20,590",
            ["RL-Mc Pherson Jumpsuit"] = "₱12,995",
            ["RL-Liza Gown"] = "₱17,495"
        };

        foreach (var brand in Brands)
        {
            var folderPath = Path.Combine(imageRootPath, brand);
            if (Directory.Exists(folderPath))
            {
                var imageFiles = Directory.GetFiles(folderPath)
                    .Select(filePath => Path.GetFileName(filePath))
                    .Where(file => file != null)
                    .Select(file =>
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file); // e.g. "B-Always Fresh Shirt"
                        var displayName = fileName
                            .Replace("OW-", "")
                            .Replace("HM-", "")
                            .Replace("PS-", "")
                            .Replace("B-", "")
                            .Replace("RL-", "")
                            .Replace("_", " ")
                            .Trim();

                        var price = allPrices.TryGetValue(fileName, out var p) ? p : "₱0";

                        return (fileName, file, displayName, price);
                    })
                    .ToList();

                ImagesByBrand[brand] = imageFiles;
            }
        }
    }
}
