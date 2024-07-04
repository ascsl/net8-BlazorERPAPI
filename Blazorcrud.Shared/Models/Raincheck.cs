using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("RaincheckGuid", Name = "IX_RaincheckGuid")]
[Index("ProductId", Name = "IX_ProductId")]
[Index("StoreId", Name = "IX_StoreId")]
public class Raincheck
{
    [Key]
    public int RaincheckId { get; private set; }
    public Guid RaincheckGuid { get; private set; } //= Guid.NewGuid();
    public string? Name { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public double SalePrice { get; set; }
    public int StoreId { get; set; }
    public bool IsDeleting { get; set; } = default!;


    [ForeignKey("ProductId")]
    [InverseProperty("Rainchecks")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Rainchecks")]
    public virtual Store Store { get; set; } = null!;

    public Raincheck()
    {
        RaincheckGuid = Guid.NewGuid();
    }
}
