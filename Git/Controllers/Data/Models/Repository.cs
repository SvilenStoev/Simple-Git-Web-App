﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data
{
    using static DataConstants;

    public class Repository
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(RepositoryMaxLength)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public bool IsPublic { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Commit> Commits { get; set; } = new List<Commit>();
    }
}
