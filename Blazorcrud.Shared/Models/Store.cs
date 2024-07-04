using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("StoreGuid", Name = "IX_StoreGuid")]
public class Store
{
    [Key]
    public int StoreId { get; private set; }
    public Guid StoreGuid { get; private set; } //= Guid.NewGuid();
    public string Name { get; set; } = default!;
    public bool IsDeleting { get; set; } = default!;

    [InverseProperty("Store")]
    public virtual ICollection<Raincheck> Rainchecks { get; set; } = new List<Raincheck>();

    public Store()
    {
        StoreGuid = Guid.NewGuid();
    }

}