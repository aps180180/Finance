using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;
namespace Finance.Web.Components.Reports
{
    public partial class FinancialSummaryChartComponent : ComponentBase
    {
        #region Properties
        [Parameter]
        public FinancialSummary? FinancialSummary { get; set; }

        [Parameter]
        public bool ShowValues { get; set; } = true;

        #endregion


        public void ToggleShowValues()
        {
            ShowValues = !ShowValues;
        }





    }
}
