using BlazorForKids.Designer.Web.Internal.Components;
using BlazorForKids.Designer.Web.Internal.Layouts;
using BlazorForKids.Designer.Web.Internal.Pages;
using BlazorForKids.Designer.Web.Widgets.Shared;
using BlazorForKidsSampleDatabase.Domain.Entities;
using BlazorForKidsSampleDatabase.Web;
using BlazorForKidsSampleDatabase.Web.Source.Components;
using BlazorForKidsSampleDatabase.Web.Source.Features.HomeFeatures.Views;
using BlazorForKidsSampleDatabase.Web.Source.Main;
using BootstrapIconsForDotNet;
using Microsoft.AspNetCore.Components;


namespace BlazorForKidsSampleDatabase.Web.Source.Features
{
    [Layout(typeof(MainLayout))]
    public partial class HomeLayout : IBkApplicationLayout
    {
       

        public void LayoutDesigner(IBkLayoutBuilder builder)
        {
            builder.CreateHeader(header =>
            {
                header.CreateMenu(menu =>
                {
                    menu.AddMenuItem<HomePage>();
                    menu.AddMenuItem<EmployeesPage>();
                    menu.AddMenuItem<DepartmentsPage>();
                });
            });
        }

        public void PagesDesigner(IBkPageBuilder builder)
        {
            builder.CreatePage("HomePage", page =>
            {
                page.PageLink.Url("/");
                page.PageTitleBar.Hide();

                page.Content(content =>
                {
                    content.DisplayComponent<WelcomeView>();
                });
            });

            builder.CreatePage("EmployeesPage", page =>
            {
                page.PageLink.Url("/employees");
                page.PageTitleBar.Hide();
                page.Content(content =>
                {
                    content.DisplayComponent<EmployeesTable>();
                });
            });

            builder.CreatePage("DepartmentsPage", page =>
            {
                page.PageLink.Url("/departments");
                page.PageTitleBar.Hide();
                page.Content(content =>
                {
                    content.DisplayComponent<DepartmentsTable>();
                });
            });
        }

        public void ComponentsDesigner(IBkComponentsBuilder builder)
        {
            builder.CreateGridView<EmployeeModel,Employee>("EmployeesTable");
            builder.CreateGridView<DepartmentModel, Department>("DepartmentsTable");
         
        }
    }
}