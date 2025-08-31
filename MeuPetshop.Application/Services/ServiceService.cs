using MeuPetShop.Domain.Dtos.ServiceDtos;
using MeuPetShop.Domain.Entities;
using MeuPetShop.Domain.Interfaces.IService;

namespace MeuPetshop.Application.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _repository;

    public ServiceService(IServiceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto serviceDto)
    {
        var newService = new Service
        {
            Name = serviceDto.Name,
            Description = serviceDto.Description,
            Price = serviceDto.Price,
            DurationInMinutes = serviceDto.DurationInMinutes
        };
        await _repository.AddAsync(newService);
        return MapServiceToDto(newService);
    }

    public async Task<ServiceDto?> GetServiceByIdAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service == null) return null;
        return MapServiceToDto(service);
    }

    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
    {
        var services = await _repository.GetAllAsync();
        return services.Select(MapServiceToDto);
    }

    public async Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto serviceDto)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service == null) return null;
        
        service.Name = serviceDto.Name;
        service.Description = serviceDto.Description;
        service.Price = serviceDto.Price;
        service.DurationInMinutes = serviceDto.DurationInMinutes;
        return MapServiceToDto(service);
    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service == null) return false;
        await _repository.DeleteAsync(service);
        return true;
    }
    
    private ServiceDto MapServiceToDto(Service service)
    {
        return new ServiceDto(service.Id, service.Name, service.Description, service.Price, service.DurationInMinutes);
    }
}