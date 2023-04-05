using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    [Index(nameof(user_email), IsUnique = true)]
    public class tbl_user
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        [StringLength(150)]
        public string? user_name { get; set; }
        [StringLength(150)]
        public string? user_email { get; set; }
        [StringLength(150)]
        public string? user_password { get; set; }
    }
}
