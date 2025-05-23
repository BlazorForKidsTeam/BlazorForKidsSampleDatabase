using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

using Microsoft.AspNetCore.Identity;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class ApplicationUser : IdentityUser, IBkEntity<ApplicationUser, ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<ApplicationUser, ApplicationDbContext> designer)
        {

        }
    }
}
