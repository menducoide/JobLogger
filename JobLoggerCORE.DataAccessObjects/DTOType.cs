using System.ComponentModel.DataAnnotations;

namespace JobLoggerCORE.DataAccessObjects
{
  public  class DTOType
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
