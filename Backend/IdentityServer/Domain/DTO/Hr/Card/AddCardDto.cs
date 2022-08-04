using System;
using Domain.Core;
using Domain.DTO.Hr.FullEmployee;
using Entities.Enum;

namespace Domain.DTO.Hr.Card
{
    public class AddCardDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public CardType CardType { get; set; }
        public string CardNumber { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public MurasalatEmployeeDto Employee { get; set; }
    }
}
