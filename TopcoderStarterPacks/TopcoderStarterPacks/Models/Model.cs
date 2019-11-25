using Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Models")]
    public class Model : IModel
    {
        public int Id { get; set; }
        
        public string Handler { get; set; }
    }
}
