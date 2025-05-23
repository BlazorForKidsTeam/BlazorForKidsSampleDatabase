using BlazorForKids.Designer.Web.Internal.Components;
using BlazorForKids.Designer.Web.Internal.Layouts;
using BlazorForKids.Designer.Web.Internal.Pages;
using BlazorForKidsSampleDatabase.Domain.Entities;

using BootstrapIconsForDotNet;

using Microsoft.AspNetCore.Components;

namespace BlazorForKidsSampleDatabase.Web.Source.Features.EmployeeFeatures
{
    [Layout(typeof(HomeLayout))]
    public partial class EmployeeInfoLayout:IBkApplicationLayout
    {

        [SupplyParameterFromQuery(Name = "EmployeeId")]
        public string? EmployeeIdParameter { get; set; }

        public EmployeeId EmployeeId => EmployeeId.ParseOrEmpty(EmployeeIdParameter);

        public void LayoutDesigner(IBkLayoutBuilder builder)
        {
            builder.CreateLeftSidePanel(panel =>
            {
                panel.CreateMenu(menu =>
                {
                    menu.AddMenuItem<EmployeeInfoEditFormPage>();
                });
            });
        }

        public void PagesDesigner(IBkPageBuilder builder)
        {
            builder.CreatePage("EmployeeInfoEditFormPage", page =>
            {
                page.PageLink.Text("Info");
                page.PageLink.Icon(Icons.INFO_CIRCLE);

                page.Content(content =>
                {
                    content.DisplayComponent<EmployeeInfoEditForm>(component =>
                    {
                        component.SetParameter(a => a.ItemKey, EmployeeId);
                    });
                });
            });
        }

        public void ComponentsDesigner(IBkComponentsBuilder builder)
        {
            builder.CreateEditForm<EmployeeInfoModel, EmployeeInfo>("EmployeeInfoEditForm");
        }
    }
}
