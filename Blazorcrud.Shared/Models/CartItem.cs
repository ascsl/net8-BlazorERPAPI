using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("CartItemGuid", Name = "IX_CartItemGuid")]
[Index("ProductId", Name = "IX_ProductId")]
public class CartItem
{
    [Key]
    public int CartItemId { get; private set; }
    public Guid CartItemGuid { get; private set; } //= Guid.NewGuid();
    public string CartId { get; set; } = null!;
    public int ProductId { get; set; }
    public int Count { get; set; }
    public bool IsDeleting { get; set; } = default!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("CartItems")]
    public virtual Product Product { get; set; } = null!;

    public CartItem()
    {
        CartItemGuid = Guid.NewGuid();
    }
}