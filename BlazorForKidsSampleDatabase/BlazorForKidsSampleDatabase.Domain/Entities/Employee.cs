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
}


