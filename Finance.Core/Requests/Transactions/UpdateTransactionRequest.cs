﻿using Finance.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {

        public long Id { get; set; }
        [Required(ErrorMessage = "Título Inválido")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tipo Inválido")]
        public ETransactionType Type { get; set; }
        [Required(ErrorMessage = "Valor Inválido")]
        public Decimal Amount { get; set; }
        [Required(ErrorMessage = "Categoria Inválida")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Data Inválida")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
