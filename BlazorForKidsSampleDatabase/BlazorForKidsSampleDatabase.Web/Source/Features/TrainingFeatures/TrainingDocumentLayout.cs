using BlazorForKids.Designer.Web.Internal.Components;
using BlazorForKids.Designer.Web.Internal.Layouts;
using BlazorForKids.Designer.Web.Internal.Pages;
using BlazorForKidsSampleDatabase.Domain.Entities;
using BootstrapIconsForDotNet;
using Microsoft.AspNetCore.Components;

namespace BlazorForKidsSampleDatabase.Web.Source.Features.TrainingFeatures
{
    [Layout(typeof(HomeLayout))]
    public partial class TrainingDocumentLayout:IBkApplicationLayout
    {
        [SupplyParameterFromQuery(Name = "TrainingDocumentId")]
        public string? TrainingDocumentIdParameter { get; set; }

        public TrainingDocumentId TrainingDocumentId => TrainingDocumentId.ParseOrEmpty(TrainingDocumentIdParameter);

        public void LayoutDesigner(IBkLayoutBuilder builder)
        {
            throw new NotImplementedException();
        }

        public void PagesDesigner(IBkPageBuilder builder)
        {
            builder.CreatePage("TrainingDocumentsPage", page =>
            {
                page.PageLink.Text("Training Documents");
                page.PageLink.Icon(Icons.FILETYPE_PDF);

                page.Content(content =>
                {
                    content.DisplayComponent<TrainingDocumentGridView>();
                });
            });

            builder.CreatePage("TrainingDocumentDepartmentsPage", page =>
            {
                page.PageLink.Text("Departments");
                page.PageLink.Icon(Icons.BUILDING);

                page.Content(content =>
                {
                    content.DisplayComponent<TrainingDocumentDepartmentsSelector>(component =>
                    {
                        component.SetParameter(a=>a.TrainingDocumentId,TrainingDocumentId);
                    });
                });
            });
        }

        public void ComponentsDesigner(IBkComponentsBuilder builder)
        {
            builder.CreateGridView<TrainingDocumentModel,TrainingDocument>("TrainingDocumentGridView", grid =>
            {
                grid.AddLinkColumn<TrainingDocumentDepartmentsPage>("Departments", link =>
                {
                    link.AddQueryParameter(a=>a.Id,"TrainingDocumentId");
                });
            });

            builder.CreateManyToManyEditor<TrainingDocument,Department>(a=>a.Departments,"TrainingDocumentDepartmentsSelector");
        }
    }
}
