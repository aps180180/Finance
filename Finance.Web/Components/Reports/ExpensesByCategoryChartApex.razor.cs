using ApexCharts;
using Finance.Core.Handlers;
using Finance.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace Finance.Web.Components.Reports
{
    public partial class ExpensesByCategoryChartApexComponent : ComponentBase
    {
        #region Properties
        public List<MyData> Data { get; set; } = [];

        public ApexChartOptions<MyData> Options { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public IReportHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            await GetExpensesByCategoryAsync();

            Options.PlotOptions = new PlotOptions
            {
                Pie = new PlotOptionsPie
                {
                    Donut = new PlotOptionsDonut
                    {
                        Labels = new DonutLabels
                        {
                            Total = new DonutLabelTotal
                            {
                                FontSize = "24px",
                                Color = "#D807B8",
                                Formatter = @"function (w) {return w.globals.seriesTotals.reduce((a, b) => { return (a + b) }, 0).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });}"
                            }
                        }
                    }
                }
            };


        }

        private async Task GetExpensesByCategoryAsync()
        {
            var request = new GetExpensesByCategoryRequest();
            var result = await Handler.GetExpensesByCategoryReportAsync(request);
            if (!result.IsSuccess || result.Data is null)
            {
                Snackbar.Add("Falha ao obter dados do relatório", Severity.Error);
                return;
            }
            foreach (var item in result.Data)
            {
                Data.Add(new MyData
                {
                    UserId = item.UserId,
                    Year = item.Year,
                    Category = item.Category,
                    Expenses = -item.Expenses
                });
            }


        }
        #endregion

        #region Public Methods
        public static string GetYAxisLabel(decimal value)
        {
            return value.ToString("C");
        }
        #endregion
        public class MyData
        {
            public string UserId { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public int Year { get; set; }
            public decimal Expenses { get; set; }
        }
    }
}
