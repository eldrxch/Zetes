using Microsoft.AspNetCore.Identity;
using Zetes.Data;

namespace Zetes.Tests;

public static class DBSeeder
{
    public static void Seed(ZetesDBContext context)
    {
        SeedCustomers(context);
        SeedProjects(context);
        SeedAppUsers(context);
    }

    private static void SeedCustomers(ZetesDBContext context)
    {
        context.Customers.AddRange(
            new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@mail.com",
                Phone = "1234567890"
            },
            new Customer
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@mail.com",
                Phone = "0987654321"
            }
        );
        context.SaveChanges();
    }

    private static void SeedProjects(ZetesDBContext context)
    {
        var customers = context.Customers.ToList();
        foreach (var customer in customers)
        {
            context.Projects.AddRange(
                new Project
                {
                    Name = "Project 1",
                    Description = "Description 1",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    CustomerId = customer.CustomerId
                },
                new Project
                {
                    Name = "Project 2",
                    Description = "Description 2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(60),
                    CustomerId = customer.CustomerId
                }
            );
        }
    }

    private static void SeedAppUsers(ZetesDBContext context)
    {
        var hasher = new PasswordHasher<AppUser>();        
        var users = new List<AppUser> {
            new AppUser
            {
                Id = "TestUserId1",
                Email = "user1@mail.com",
                UserName = "user1@mail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,                
            },
            new AppUser
            {
                Id = "TestUserId2",
                Email = "user1@mail.com",
                UserName = "user1@mail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            }
        };

        foreach (var user in users)
        {
            user.PasswordHash = hasher.HashPassword(user, "P@ssw0rd");
        }

        context.AppUsers.AddRange(users);
        context.SaveChanges();
    }
}