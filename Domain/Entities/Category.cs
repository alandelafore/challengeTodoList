using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("categories")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

}

