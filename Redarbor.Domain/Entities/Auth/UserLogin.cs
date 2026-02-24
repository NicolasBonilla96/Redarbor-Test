using Microsoft.AspNetCore.Identity;

namespace Redarbor.Domain.Entities.Auth;

public class UserLogin : IdentityUserLogin<Guid>
{
	#region Properties

	public DateTime LoginDate { get; set; }

	#endregion

	#region Relations

	public virtual User User { get; set; }

	#endregion
}
