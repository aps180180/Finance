using ApexCharts;
using Finance.Core.Handlers;
using Finance.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Globalization;
using System.Reflection.Emit;

namespace Finance.Web.Components.Reports
{
    public partial class IncomesAndExpensesChartApexComponent : ComponentBase
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
                Data.Add(new MyData
                {
                    UserId = item.UserId,
                    Year = item.Year,
                    Expenses = -item.Expenses,
                    Incomes = item.Incomes,
                    Category = GetMonthName(item.Month)

                });
            }

            Data = Data.OrderBy(e => MonthOrder(e.Category)).ToList();

        }
        #endregion
        private static string GetMonthName(int month) =>
            new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM", CultureInfo.CurrentCulture);

        private int MonthOrder(string month)
        {
            return month switch
            {
                "Janeiro" => 1,
                "Fevereiro" => 2,
                "Março" => 3,
                "Abril" => 4,
                "Maio" => 5,
                "Junho" => 6,
                "Julho" => 7,
                "Agosto" => 8,
                "Setembro" => 9,
                "Outubro" => 10,
                "Novembro" => 11,
                "Dezembro" => 12,
                _ => 0
            };
        }

        public class MyData
        {
            public string UserId { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public int Year { get; set; }
            public decimal Incomes { get; set; }
            public decimal Expenses { get; set; }
        }
    }
}
