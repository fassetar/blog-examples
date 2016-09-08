
 
 

 

/// <reference path="Enums.ts" />

declare module Microsoft.AspNet.Identity {
	interface UserLoginInfo {
		LoginProvider: string;
		ProviderKey: string;
	}
}
declare module Microsoft.AspNet.Identity.EntityFramework {
	interface IdentityUser extends Microsoft.AspNet.Identity.EntityFramework.IdentityUser<string, Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin, Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole, Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim> {
	}
	interface IdentityUser<TKey, TLogin, TRole, TClaim> {
		AccessFailedCount: number;
		Claims: TClaim[];
		Email: string;
		EmailConfirmed: boolean;
		Id: TKey;
		LockoutEnabled: boolean;
		LockoutEndDateUtc: Date;
		Logins: TLogin[];
		PasswordHash: string;
		PhoneNumber: string;
		PhoneNumberConfirmed: boolean;
		Roles: TRole[];
		SecurityStamp: string;
		TwoFactorEnabled: boolean;
		UserName: string;
	}
	interface IdentityUserClaim extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string> {
	}
	interface IdentityUserClaim<TKey> {
		ClaimType: string;
		ClaimValue: string;
		Id: number;
		UserId: TKey;
	}
	interface IdentityUserLogin extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string> {
	}
	interface IdentityUserLogin<TKey> {
		LoginProvider: string;
		ProviderKey: string;
		UserId: TKey;
	}
	interface IdentityUserRole extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string> {
	}
	interface IdentityUserRole<TKey> {
		RoleId: TKey;
		UserId: TKey;
	}
}
declare module TypeLiteExample.Models {
	interface ApplicationUser extends Microsoft.AspNet.Identity.EntityFramework.IdentityUser {
	}
	interface ExternalLoginConfirmationViewModel {
		Email: string;
	}
	interface IndexViewModel {
		BrowserRemembered: boolean;
		HasPassword: boolean;
		Logins: Microsoft.AspNet.Identity.UserLoginInfo[];
		PhoneNumber: string;
		TwoFactor: boolean;
	}
}



declare module TypeLiteExample.Models {
	interface ExternalLoginConfirmationViewModel {
		Email: string;
	}
}
