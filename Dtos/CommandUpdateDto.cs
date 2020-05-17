using System;

namespace Commander.Dtos
{
    public class CommandUpdateDto : CommandCreateDto
    {
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
