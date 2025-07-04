﻿namespace DTOs;

public class LoginResponseDto
{
    public int UserId { get; set; }
    public string Account { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}