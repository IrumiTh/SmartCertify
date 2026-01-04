using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCertify.Application.DTOs
{
    public class BulkCreateChiceDTO
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one choice is required.")]
        public List<CreateChoiceDto> Choices { get; set; } = new();

    }
}
