using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Application.Features.Roles.Dtos;
using Redarbor.Application.Features.Users.Dtos;
using Redarbor.Domain.Entities.Auth;
using System.Net;

namespace Redarbor.Infrastructure.Services;

public class UserRoleService(
        IServiceProvider provider
    ) : IUserRoleService
{
    private readonly UserManager<User> _userManager = provider.GetRequiredService<UserManager<User>>();
    private readonly RoleManager<Role> _roleManager = provider.GetRequiredService<RoleManager<Role>>();

    public async Task<Result<User>> AddUser(CreateUserRequest user)
    {
        var existUser = await _userManager.FindByNameAsync(user.UserName);
        if (existUser is not null)
            return Result<User>.Failure($"Ya existe un usuario creado bajo el nombre '{user.UserName}'.", (int)HttpStatusCode.Conflict);

        existUser = await _userManager.FindByEmailAsync(user.Email);
        if (existUser is not null)
            return Result<User>.Failure($"Ya existe un usuario creado bajo el correo '{user.Email}'.", (int)HttpStatusCode.Conflict);

        if (!await _roleManager.RoleExistsAsync(user.Role))
            return Result<User>.Failure($"No existe ningún rol bajo el nombre '{user.Role}'.", (int)HttpStatusCode.Conflict);

        var newUser = User.CreateUser(user.UserName, user.Email, user.PhoneNumber);
        
        var resp = await _userManager.CreateAsync(newUser, user.Password);
        if (!resp.Succeeded)
            return Result<User>.Failure(resp.Errors.FirstOrDefault()!.Description, (int)HttpStatusCode.Conflict);

        var respAddRole = await _userManager.AddToRoleAsync(newUser, user.Role);
        if (!respAddRole.Succeeded)
            return Result<User>.Failure(respAddRole.Errors.FirstOrDefault()!.Description, (int)HttpStatusCode.Conflict);

        return Result<User>.Success(newUser);
    }

    public async Task<Result<User>> UpdateUser(CreateUserRequest user, User userUpdate)
    {
        userUpdate.UserName = user.UserName;
        userUpdate.Email = user.Email;
        userUpdate.PhoneNumber = user.PhoneNumber;

        var respUpdate = await _userManager.UpdateAsync(userUpdate);
        if(!respUpdate.Succeeded)
            return Result<User>.Failure(respUpdate.Errors.FirstOrDefault()!.Description, (int)HttpStatusCode.Conflict);

        if(!await _userManager.CheckPasswordAsync(userUpdate, user.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(userUpdate);
            var updatePass = await _userManager.ResetPasswordAsync(userUpdate, token, user.Password);
            if (!updatePass.Succeeded)
                return Result<User>.Failure(respUpdate.Errors.FirstOrDefault()!.Description, (int)HttpStatusCode.Conflict);
        }

        var userRole = await _userManager.GetRolesAsync(userUpdate);
        if(!userRole.Count.Equals(0) && !userRole.FirstOrDefault().Equals(user.Role))
        {
            await _userManager.RemoveFromRoleAsync(userUpdate, userRole.FirstOrDefault()!);
            await _userManager.AddToRoleAsync(userUpdate, user.Role);
        }

        return Result<User>.Success(userUpdate);
    }

    public async Task<Result<Role>> AddRole(CreateRoleRequest role)
    {
        if (await _roleManager.RoleExistsAsync(role.Name))
            return Result<Role>.Failure($"Ya existe un role creado bajo el nombre '{role.Name}'.", (int)HttpStatusCode.Conflict);

        var newRole = Role.CreateRole(role.Name, role.Description);
        var resp = await _roleManager.CreateAsync(newRole);
        if(!resp.Succeeded)
            return Result<Role>.Failure(resp.Errors.FirstOrDefault()!.Description, (int)HttpStatusCode.Conflict);

        return Result<Role>.Success(newRole);
    }

    public async Task<Result<User>> GetUser(Guid userId)
    {
        var existUser = await _userManager.FindByIdAsync(userId.ToString());
        if(existUser is null)
            return Result<User>.Failure($"No se encontró ningún usuario con el id '{userId}'", (int)HttpStatusCode.NotFound);

        return Result<User>.Success(existUser);
    }
}
