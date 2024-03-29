﻿using Airbnb.Application.Interfaces;
using Airbnb.Application.Services;
using Airbnb.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Airbnb.Application.Requests.Auth.Commands.RegisterEmail
{
    public class RegisterEmailCommandHandler : IRequestHandler<RegisterEmailCommand, string>
    {
        private readonly IJwtService _jwtService;
        private readonly AirbnbContext _dbContext;
        public RegisterEmailCommandHandler(IJwtService jwtService, AirbnbContext airbnbContext)
        {
            _jwtService = jwtService;
            _dbContext = airbnbContext;
        }


        public async Task<string> Handle(RegisterEmailCommand command, CancellationToken cancellationToken)
        {
            await CheckEmailExists(command.Email);
            var commonRoleIds = new int[] { 0, 1, 3 };
            var defaultRoles = await _dbContext.Roles.AsNoTracking().Where(r => commonRoleIds.Contains(r.RoleId)).ToArrayAsync(cancellationToken);

            var hashed = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new User
            {
                SexId = command.Sex,
                StreetId = 100,
                BornDate = command.BornDate,
                Email = command.Email,
                NormEmail = command.Email.ToUpper(),
                EmailVerificationToken = _jwtService.GenerateRandomToken(),
                FirstName = command.FirstName,
                SecondName = command.SecondName,
                MiddleName = command.MiddleName,
                HashedPassword = hashed,
                ReceiveNews = command.RecieveNews,
            };

            foreach (var role in defaultRoles)
            {
                user.UsersRoles.Add(new UsersRole { RoleId = role.RoleId });
            }
            var entity = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var token =  _jwtService.Generate(entity.Entity, defaultRoles);

            return token;
        }

        private async Task CheckEmailExists(string email)
        {
            var normEmail = email.ToUpper();
            var exists = await _dbContext.Users.AsNoTracking().AnyAsync(u => u.Email != null && u.NormEmail == normEmail);
            if (exists)
                throw new ValidationException(new ValidationFailure[] { new ValidationFailure("Email", "a user with the same email already exists") });
        }

    }
}
