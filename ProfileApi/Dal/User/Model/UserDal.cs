﻿namespace Dal.User.Model;

public class UserDal
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public Guid Id { get; internal set; }
}