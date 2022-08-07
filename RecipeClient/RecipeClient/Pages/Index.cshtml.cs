using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string ApiUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger,IConfiguration config)
        {
            _logger = logger;
            ApiUrl = config.GetRequiredSection("url").Get<string>();
        }

        public void OnGet()
        {

        }
    }
}