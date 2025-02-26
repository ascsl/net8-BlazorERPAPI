using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Shared.Models;

[Index("OrderGuid", Name = "IX_OrderGuid")]
public class Order
{
    [Key]
    public int OrderId { get; private set; }
    public Guid OrderGuid { get; private set; } //= Guid.NewGuid();

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }
    public string Username { get; set; } = null!;

    [StringLength(160)]
    public string Name { get; set; } = null!;

    [StringLength(70)]
    public string Address { get; set; } = null!;

    [StringLength(40)]
    public string City { get; set; } = null!;

    [StringLength(40)]
    public string State { get; set; } = null!;

    [StringLength(10)]
    public string PostalCode { get; set; } = null!;

    [StringLength(40)]
    public string Country { get; set; } = null!;

    [StringLength(24)]
    public string Phone { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }
    public bool IsDeleting { get; set; } = default!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public Order()
    {
        OrderGuid = Guid.NewGuid();
    }
}