
using System;

namespace ToDo.Api.Models.Entities
{
    public class ModelBase
    {
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpDatedAt { get; set; }
    }
}
