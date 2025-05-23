using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

using BlazorForKidsSampleDatabase.Domain.Shared;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class Employee:IBkEntity<Employee,ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<Employee, ApplicationDbContext> designer)
        {
            designer.Properties.AddTextProperty<string>("FirstName");
            designer.Properties.AddTextProperty<string>("LastName");

            designer.Properties.AddOneEntity<Department>("Department");

            designer.Properties.AddDependantEntity<EmployeeInfo>("EmployeeInfo");
        }
    }

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


