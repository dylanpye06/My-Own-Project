﻿using System.Reflection;

namespace Osiansdrystonewalls.com.Models.Domain
{
    public class JobRequest
    {
        public Guid Id { get; set; }
        public required string JobName { get; set; }
        public required string Description { get; set; }
    }
}
