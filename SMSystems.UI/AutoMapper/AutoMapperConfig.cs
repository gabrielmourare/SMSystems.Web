// No seu namespace UI
using AutoMapper;
using SMSystems.Domain.Entities;
using SMSystems.UI.ViewModels;

public class AutoMapperConfig
{
    public static IMapper Initialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PatientViewModel, Patient>();
            cfg.CreateMap<Patient, PatientViewModel>();
            // Adicione outros mapeamentos conforme necessário
        });

        return config.CreateMapper();
    }
}
