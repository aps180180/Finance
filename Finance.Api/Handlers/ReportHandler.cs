using Finance.Api.Data;
using Finance.Core.Enums;
using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Finance.Core.Requests.Reports;
using Finance.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finance.Api.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context.ExpensesByCategories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderByDescending(x => x.Year)
                    .ThenBy(x => x.Category)
                    .ToListAsync();
                
                return new Response<List<ExpensesByCategory>?>(data);
            }
            catch 
            {

                return new Response<List<ExpensesByCategory>?>(null, 500, "Não foi possivel retornar as despesas por categoria");
            }
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
           // resumo financeiro do mes//
           var startDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            try
            {
                var data = await context.Transacitions
                 .AsNoTracking()
                 .Where(x => x.UserId == request.UserId
                     && x.PaidOrReceivedAt >= startDate
                     && x.PaidOrReceivedAt <= DateTime.Now
                  )
                 .GroupBy(x => true)
                 .Select(x => new FinancialSummary(
                    request.UserId,
                    x.Where(tp => tp.Type == ETransactionType.Entrada).Sum(t => t.Amount),
                    x.Where(tp => tp.Type == ETransactionType.Saida).Sum(t => t.Amount))
                 ).FirstOrDefaultAsync();

                return new Response<FinancialSummary?>(data);

            }
            catch 
            {

                return new Response<FinancialSummary?>(null, 500, "Não foi possível retornar o resumo financeiro");  
            }     
            


        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context.IncomesAndExpenses.
                AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();

                return new Response<List<IncomesAndExpenses>?>(data);
            }
            catch  
            {

                return new Response<List<IncomesAndExpenses>?>(null, 500, "Não foi possível obter as entradas e saídas");
            }
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context
                    .IncomesByCategories
                    .AsNoTracking()
                    .Where(x=> x.UserId == request.UserId)
                    .OrderByDescending(x => x.Year)
                    .ThenBy(x=> x.Category)
                    .ToListAsync();
                return new Response<List<IncomesByCategory>?>(data);
            }
            catch 
            {

                return new Response<List<IncomesByCategory>?>(null, 500, "Não foi possível retornar as entradas por categoria");
            }
        }
    }
}
