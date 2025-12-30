using Chirp.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chirp.Services;

/// <summary>
/// Extends <see cref="SignInManager{TUser}"/> to provide email-based sign-in functionality
/// for authors in the Chirp system.
/// </summary>
/// <remarks>
/// This class customizes the standard ASP.NET Core Identity <see cref="SignInManager{TUser}"/>
/// to allow signing in users via their email address instead of their username.
/// </remarks>
public class EmailSignInManager(
    UserManager<Author> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<Author> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<Author>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<Author> confirmation)
    : SignInManager<Author>(userManager,
        contextAccessor,
        claimsFactory,
        optionsAccessor,
        logger,
        schemes,
        confirmation)
{
    /// <summary>
    /// Attempts to sign in a user using their email and password.
    /// </summary>
    /// <param name="email">The email address of the user attempting to sign in.</param>
    /// <param name="password">The password to authenticate with.</param>
    /// <param name="isPersistent">
    /// A boolean indicating whether the authentication session should persist
    /// after the browser is closed.
    /// </param>
    /// <param name="lockoutOnFailure">
    /// A boolean indicating whether the user account should be locked if
    /// sign-in fails multiple times.
    /// </param>
    /// <returns>
    /// A <see cref="Task{SignInResult}"/> representing the asynchronous operation,
    /// containing the result of the sign-in attempt.
    /// </returns>
    /// <remarks>
    /// This method first finds the user by email using <see cref="UserManager{TUser}.FindByEmailAsync"/>
    /// and then delegates the password validation to the base <see cref="SignInManager{TUser}.PasswordSignInAsync"/> method.
    /// If the user does not exist, <see cref="SignInResult.Failed"/> is returned.
    /// </remarks>
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
