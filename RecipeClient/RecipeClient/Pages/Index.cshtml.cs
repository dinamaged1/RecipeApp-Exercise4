using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace RecipeClient.Pages
{
    public class Image
    {
        public string FileName { get; set; } = string.Empty;
        public string Base64String { get; set; }= string.Empty;
    }

    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _host;
        [BindProperty]
        public string ApiUrl { get; set; }

        
        public IndexModel(ILogger<IndexModel> logger,IConfiguration config, IWebHostEnvironment host)
        {
            _logger = logger;
            ApiUrl = config.GetRequiredSection("url").Get<string>();
            _host = host;
        }

        public async Task OnPostSaveImageToFolder([FromBody] Image recievedImage)
        {
            byte[] imageBytes = Convert.FromBase64String(recievedImage.Base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);
            var data = ms.ToArray();
            var filePath = $"{_host.WebRootPath}/RecipesImages/{recievedImage.FileName}";
            await System.IO.File.WriteAllBytesAsync(filePath, data);
        }

        public async Task OnPostDeleteImageFolder([FromBody] Image recievedImage)
        {
            //Delete Image from folder RecipeImages
            var filePathDelete = $"{_host.WebRootPath}{recievedImage.FileName}";
            System.IO.File.Delete(filePathDelete);
        }
    }
}