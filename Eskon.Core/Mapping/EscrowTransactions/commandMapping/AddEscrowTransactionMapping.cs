using Eskon.Domian.DTOs.EscrowTransaction;
using Eskon.Domian.Entities;


namespace Eskon.Core.Mapping.EscrowTransactions
{
    public partial class EscrowTransactionProfileMapping
    {
        public void AddEscrowTransactionMapping()
        {
            CreateMap<AddEscrowTransactionDTO, EscrowTransaction>();
        }
    }
}
