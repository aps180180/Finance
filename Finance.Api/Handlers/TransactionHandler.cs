using Finance.Api.Data;
using Finance.Core.Common.Extensions;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Transactions;
using Finance.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Finance.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type


                };

                await context.Transacitions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch 
            {

                return new Response<Transaction?>(null,500, "Falha ao criar a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transacitions
                 .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada!");
                context.Transacitions.Remove(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, message: "Transação excluída com sucesso!");
            }

            catch
            {

                return new Response<Transaction?>(null, 500, "Não foi possível  excluir a transação!");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transacitions
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

                return transaction is null
                  ? new Response<Transaction?>(null, 404, "Transação não encontrada!")
                  : new Response<Transaction?>(transaction);

            }
            catch 
            {

                return new Response<Transaction?>(null, 500, "Não foi possível  buscar a transação!");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de início ou termino da busca");
                
            }

            try
            {
                var query = context
               .Transacitions
               .AsNoTracking()
               .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt
                        <= request.EndDate && x.UserId == request.UserId)
               .OrderBy(x => x.CreatedAt);

                var transactions = await query
                       .Skip((request.PageNumber - 1) * request.PageNumber)
                       .Take(request.PageSize)
                       .ToListAsync();

                var count = await query.CountAsync();
                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Ocorreu um erro ao buscar as Transações");
            }


        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {

            try
            {
                var transaction = await context.Transacitions
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);
                if (transaction is null)
                    return new Response<Transaction?>(null, 400, "Transação não encontrada!");

                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                
                context.Transacitions.Update(transaction);
                await context.SaveChangesAsync();  
                
                return new Response<Transaction?>(transaction);

            }
            catch
            {

                return new Response<Transaction?>(null, 500, "Não foi possível  buscar a transação!");
            }


        }
    }
}
