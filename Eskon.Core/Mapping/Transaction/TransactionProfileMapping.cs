using AutoMapper;
using Eskon.Domian.Models;
using Eskon.Domian.DTOs.Transaction;
using Eskon.Domain.DTOs.Transaction;
using Microsoft.Data.SqlClient;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        // Map for WriteDTO
        CreateMap<Transaction, TransactionWriteDTO>()
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Amount + src.Fee))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.FirstName + src.Sender.LastName)) 
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.FirstName + src.Receiver.LastName));

        // Map for ReadDTO
        CreateMap<Transaction, TransactionReadDTO>()
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Amount + src.Fee))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.FirstName + src.Sender.LastName))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.FirstName + src.Receiver.LastName));
    }
}
