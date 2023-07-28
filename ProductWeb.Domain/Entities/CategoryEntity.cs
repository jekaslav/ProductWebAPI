namespace ProductWeb.Domain.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<ProductEntity> Products { get; set; }
}