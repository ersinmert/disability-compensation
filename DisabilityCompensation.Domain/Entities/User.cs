﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }

        #region Relations

        public List<Compensation>? Compensations { get; set; }
        public List<UserAuthority>? UserAuthorities { get; set; }
        public List<UserRole>? UserRoles { get; set; }

        #endregion
    }
}
