using System;
using System.Collections.Generic;

namespace BookCatalog.API.Models;

public partial class Genre
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
