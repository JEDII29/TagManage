﻿using System.ComponentModel.DataAnnotations;

namespace TagManage.API.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
