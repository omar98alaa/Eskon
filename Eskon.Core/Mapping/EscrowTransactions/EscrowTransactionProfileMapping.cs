using AutoMapper;

namespace Eskon.Core.Mapping.EscrowTransactions
{
    public partial class EscrowTransactionProfileMapping : Profile
    {
        public EscrowTransactionProfileMapping()
        {
            AddEscrowTransactionMapping();
        }
    }
}
