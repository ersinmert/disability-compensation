﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Authority")]
    public class Authority : BaseEntity
    {
        public string? Name { get; set; }

        #region Relations
        public List<RoleAuthority>? RoleAuthorities { get; set; }

        #endregion
    }
}
