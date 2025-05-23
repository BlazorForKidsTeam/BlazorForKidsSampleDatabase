using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class Employee:IBkEntity<Employee,ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<Employee, ApplicationDbContext> designer)
        {
            designer.Properties.AddTextProperty<string>("FirstName");
            designer.Properties.AddTextProperty<string>("LastName");

            designer.Properties.AddOneEntity<Department>("Department");

            
        }
    }

   
}


