using MeuPetShop.Domain.Dtos.ServiceDtos;

namespace MeuPetShop.Domain.Interfaces.IService;

public interface IServiceService
{
    Task<ServiceDto> CreateServiceAsync(CreateServiceDto serviceDto);
    Task<ServiceDto?> GetServiceByIdAsync(int id);
    Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
    Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto serviceDto);
    Task<bool> DeleteServiceAsync(int id);
}