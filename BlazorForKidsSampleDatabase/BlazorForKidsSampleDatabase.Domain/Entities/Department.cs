using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class Department:IBkEntity<Department,ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<Department, ApplicationDbContext> designer)
        {
            designer.DefaultCatalogTextProperty(a=>a.Name);
            designer.Properties.AddTextProperty<string>("Name");
        }
    }
}
