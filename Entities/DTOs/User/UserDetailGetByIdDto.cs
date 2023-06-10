using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DTOs.User
{
    public class UserDetailGetByIdDto : IDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [JsonIgnore] public string? Password { get; set; }

        [JsonIgnore] public string? RePassword { get; set; }
    }
}
