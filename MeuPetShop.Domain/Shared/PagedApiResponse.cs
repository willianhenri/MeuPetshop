using MeuPetShop.Domain.Interfaces.IProducts;

namespace MeuPetShop.Domain.Shared;

public class PagedApiResponse<T> : ApiResponse<IEnumerable<T>>
{
    public PaginationData? Pagination { get; set; }
}