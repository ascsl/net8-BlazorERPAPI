using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("ProductGuid", Name = "IX_ProductGuid")]
[Index("CategoryId", Name = "IX_CategoryId")]
public class Product
{
    [Key]
    public int ProductId { get; private set; }
    public Guid ProductGuid { get; private set; } //= Guid.NewGuid();
    public string? SkuNumber { get; set; } = null!;
    public int CategoryId { get; set; }
    public int RecommendationId { get; set; }
    public bool IsDeleting { get; set; } = default!;

    [StringLength(160)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal SalePrice { get; set; }

    [StringLength(1024)]
    public string? ProductArtUrl { get; set; }

    public string? Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; }

    public string? ProductDetails { get; set; } = null!;

    public int Inventory { get; set; }

    public int LeadTime { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Raincheck> Rainchecks { get; set; } = new List<Raincheck>();

    public Product()
    {
        ProductGuid = Guid.NewGuid(); // Genera un GUID nuevo cuando se crea una nueva instancia
        CategoryId = 1;
        Price = 5;
        SalePrice = 9;
        Inventory = 15;
        LeadTime = 25;
        Created = DateTime.UtcNow;
    }
}

