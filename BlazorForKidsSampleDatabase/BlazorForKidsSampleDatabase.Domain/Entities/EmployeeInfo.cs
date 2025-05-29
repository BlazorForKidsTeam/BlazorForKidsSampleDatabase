using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

using BlazorForKidsSampleDatabase.Domain.Shared;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class EmployeeInfo : IBkEntity<EmployeeInfo, ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<EmployeeInfo, ApplicationDbContext> designer)
        {
            designer.Properties.AddImageProperty("Image");

            designer.Properties.AddDateProperty<DateOnly>("DateOfHire");
            designer.Properties.AddEnumProperty<Gender>("Geneder");

            designer.Properties.AddNumberProperty<int>("BadgeNumber"); //1,2,3
            designer.Properties.AddNumberProperty<decimal>("Salary"); // 9999999999.99

            designer.Properties.AddBoolProperty<bool>("IsActive");
        }
    }
}


