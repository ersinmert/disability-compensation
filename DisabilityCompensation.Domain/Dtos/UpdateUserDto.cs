﻿namespace DisabilityCompensation.Domain.Dtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
    }
}
