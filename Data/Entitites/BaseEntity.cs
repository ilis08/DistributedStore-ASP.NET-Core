using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

        public int UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
