namespace MeuPetShop.Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description  { get; set; }
    public decimal Price { get; set; }
    public DateTime DateAdded { get; set; }
    public int StockQuantity  { get; set; }
}