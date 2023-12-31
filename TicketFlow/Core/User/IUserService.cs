﻿using Microsoft.AspNetCore.Identity;
using TicketFlow.Core.Dtos;

namespace TicketFlow.Core.User;

public interface IUserService
{
    Task<bool> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest);
    Task<List<UserRoleResponse>> GetUsersAsync();
    Task ChangePasswordAsAdminAsync(ChangePasswordAsAdminRequest changePasswordAsAdminRequest);
    Task<IdentityUser> GetUserInSessionAsync();
    Task<UserRoleResponse> GetUserByIdAsync(string userId);
}