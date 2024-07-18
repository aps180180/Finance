using Finance.Core.Handlers;
using Finance.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Globalization;

namespace Finance.Web.Components.Reports
{
    public partial class IncomesAndExpensesChartComponent : ComponentBase
    {
        #region Properties
        public ChartOptions Options { get; set; } = new();
        public List<ChartSeries>? Series { get; set; }
        public List<string> Labels { get; set; } = [];
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
           await GetIncomesAndExpensesAsync();

        }

        private async Task GetIncomesAndExpensesAsync()
        {
            var request = new GetIncomesAndExpensesRequest();

            var result = await Handler.GetIncomesAndExpensesReportAsync(request);
            if (!result.IsSuccess || result.Data is null)
            {
                Snackbar.Add("Não foi possível retornar os dados do relatório", Severity.Error);
                return;
            }
            var Incomes = new List<double>();
            var Expenses = new List<double>();

            foreach (var item in result.Data)
            {
                Incomes.Add((double)item.Incomes);
                Expenses.Add(-(double)item.Expenses);
                Labels.Add(GetMonthName(item.Month));
            }

            Options.YAxisTicks = 1000; // linhas de escala de baixo pra cima
            
            Options.LineStrokeWidth = 5;
            Options.ChartPalette = ["#76FF01", Colors.Red.Default];

            Series = [
                    new ChartSeries{Name="Receitas",Data=Incomes.ToArray()},
                    new ChartSeries{Name="Despesas",Data= Expenses.ToArray()}

            ];
            
        }
        #endregion
        private static string GetMonthName(int month) =>
            new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM", CultureInfo.CurrentCulture);

    }
}
