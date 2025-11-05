using Chirp.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Chirp.Web.Util;

public class EmailSignInManager : SignInManager<Author>
{
    public EmailSignInManager(UserManager<Author> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<Author> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<Author>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<Author> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes,
            confirmation)
    {
    }

    /// <summary>
    /// Attempts to sign in the specified <paramref name="email"/> and <paramref name="password"/> combination
    /// as an asynchronous operation.
    /// </summary>
    /// <param name="email">The email to sign in.</param>
    /// <param name="password">The password to attempt to sign in with.</param>
    /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
    /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
    /// <returns>The task object representing the asynchronous operation containing the <see name="SignInResult"/>
    /// for the sign-in attempt.</returns>
    public override async Task<SignInResult> PasswordSignInAsync(string email, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
}
