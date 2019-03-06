using System.ComponentModel.DataAnnotations;

namespace JobLoggerCORE.DataAccessObjects
{
   public class DTOLogMessage
    {
        public int Id { get; set; }
        [Display(Name = "Message")]
        [Required]
        public string Message { get; set; }
        [Display(Name = "Type")]
        public int IdType { get; set; }

    }
}
