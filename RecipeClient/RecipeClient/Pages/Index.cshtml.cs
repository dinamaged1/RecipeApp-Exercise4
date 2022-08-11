using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace RecipeClient.Pages
{
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

        public void OnGet()
        {

        }

        
        public async Task OnPostSaveImageToFolder(string base64string, string filename)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);
            var data = ms.ToArray();
            var filePath = $"{_host.WebRootPath}/RecipesImages/{filename}";
            await System.IO.File.WriteAllBytesAsync(filePath, data);
        }
    }
}