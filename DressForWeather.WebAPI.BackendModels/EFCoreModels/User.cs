using Microsoft.AspNetCore.Identity;

namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class User : IdentityUser<long>
{
	/* 
	 * from IdentityUser:
	 * 
	 * public virtual <long>  Id { get; set; } = default!;
	 * public virtual string? UserName { get; set; }
	 * public virtual string? NormalizedUserName { get; set; }
	 * public virtual string? Email { get; set; }
	 * public virtual string? NormalizedEmail { get; set; }
	 * public virtual bool    EmailConfirmed { get; set; }
	 * public virtual string? PasswordHash { get; set; }
	 * public virtual string? SecurityStamp { get; set; }
	 * public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
	 * public virtual string? PhoneNumber { get; set; }
	 * public virtual bool    PhoneNumberConfirmed { get; set; }
	 * public virtual bool    TwoFactorEnabled { get; set; }
	 * public virtual DateTimeOffset? LockoutEnd { get; set; }
	 * public virtual bool    LockoutEnabled { get; set; }
	 * public virtual int     AccessFailedCount { get; set; }
	 * 
	 * public override string ToString() => UserName ?? string.Empty;
	 */
}