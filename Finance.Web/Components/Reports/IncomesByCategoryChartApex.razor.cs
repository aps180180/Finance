using ApexCharts;
using Finance.Core.Handlers;
using Finance.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace Finance.Web.Components.Reports
{
    public partial class IncomesByCategoryChartApexComponent : ComponentBase
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
            await GetIncomesByCategoryAsync();

            Options.PlotOptions = new PlotOptions
            {
                Pie = new PlotOptionsPie
                {
                    Donut = new PlotOptionsDonut
                    {
                        Labels = new DonutLabels
                        {
                            Total = new DonutLabelTotal { FontSize = "24px", Color = "#D807B8", Formatter = @"function (w) {return w.globals.seriesTotals.reduce((a, b) => { return (a + b) }, 0).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });}" }
                        }
                    }
                }
            };

            
        }

        private async Task GetIncomesByCategoryAsync()
        {
            var request = new GetIncomesByCategoryRequest();
            var result = await Handler.GetIncomesByCategoryReportAsync(request);
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
                    Incomes = item.Incomes
                });
            }


        }
        #endregion

        #region Public Methods
        public static string GetYAxisLabel(decimal value)
        {
            return  value.ToString("C");
        }
        #endregion
        public class MyData
        {
            public string UserId { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public int Year { get; set; }
            public decimal Incomes { get; set; }
        }
    }
}
