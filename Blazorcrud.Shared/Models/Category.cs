using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("CategoryGuid", Name= "IX_CategoryGuid")]
public class Category
{
    [Key]
    public int CategoryId { get; private set; }
    public Guid CategoryGuid { get; private set; } //= Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleting { get; set; } = default!;

    public List<Product> Products { get; set; }

//    [InverseProperty("Category")]
//    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public Category()
    {
        CategoryGuid = Guid.NewGuid();
    }
}
