using Identity.Razor.V2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Razor.V2.Pages.Account;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<User> userManager;

    [BindProperty]
    public string? Message { get; set; }

    public ConfirmEmailModel(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string token)
    {
        var user = await this.userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var result = await this.userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                this.Message = "Email address is successfully confirmed, now you can login.";
            }
            return Page();
        }

        this.Message = "Failed to validate the email";
        return Page();
    }
}
