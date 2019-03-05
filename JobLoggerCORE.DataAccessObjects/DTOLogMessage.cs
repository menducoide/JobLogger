using System.ComponentModel.DataAnnotations;

namespace JobLoggerCORE.DataAccessObjects
{
   public class DTOLogMessage
    {
        public int Id { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Type")]
        public int IdType { get; set; }

    }
}
