using Microsoft.AspNetCore.Identity;

namespace Redarbor.Domain.Entities.Auth;

public class UserRole : IdentityUserRole<Guid>
{
	#region Relations

	public virtual User User { get; set; }

	public virtual Role Role { get; set; }

	#endregion
}
