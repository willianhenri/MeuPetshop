using MeuPetShop.Domain.Interfaces.IProdutos;

namespace MeuPetShop.Domain.Shared;

public class PagedApiResponse<T> : ApiResponse<IEnumerable<T>>
{
    public PaginationData? Pagination { get; set; }
}