using Microsoft.EntityFrameworkCore;

namespace EFModeling.FluentAPI.Relational.IndexName
{
    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }

    #region IndexName
    [Index(nameof(Url), Name = "Index_Url")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
    #endregion
}
