using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("LICENCES")]
    public class Licence
    {
        [Column("KEY")]
        public int Key { get; set; }
        [Column("STATUSID")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; } = null!;
    }
}
