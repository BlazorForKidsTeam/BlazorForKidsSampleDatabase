using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForKids.Designer.Domain;
using BlazorForKids.Designer.Domain.Internal;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class TrainingDocument:IBkEntity<TrainingDocument,ApplicationDbContext>
    {
        public void BkConfiguration(IBkEntityDesigner<TrainingDocument, ApplicationDbContext> designer)
        {
            designer.Properties.AddTextProperty<string>("Name");
            designer.Properties.AddFileProperty("DocumentFile");

            designer.Properties.AddManyEntities<Department>("Departments", s =>
            {
                s.NameOfMany("Documents");
            });
        }
    }
}
