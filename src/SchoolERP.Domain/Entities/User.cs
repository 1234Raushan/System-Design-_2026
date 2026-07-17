using SchoolERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Entities
{
    public sealed class User : BaseAuditableEntity
    {
        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string UserName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string? PhoneNumber { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public bool PhoneNumberConfirmed { get; private set; }

        public DateTime? LastLoginDate { get; private set; }
        public int RoleId { get; private set; }

        // Navigation Property
        public Role Role { get; private set; } = null!;

        // EF Core
        private User()
        {
        }

        public User(
            string firstName,
            string lastName,
            string email,
            string userName,
            string passwordHash,
            int roleId,
            string? phoneNumber = null)
        {
            UpdateName(firstName, lastName);
            SetEmail(email);
            SetUserName(userName);

            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;

            AssignRole(roleId);

            CreatedDate = DateTime.UtcNow;
        }

        public void AssignRole(int roleId)
        {
            if (roleId <= 0)
                throw new ArgumentOutOfRangeException(nameof(roleId));

            RoleId = roleId;
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            Email = email.Trim().ToLowerInvariant();
        }

        public void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username is required.");

            UserName = userName.Trim();
        }

        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void UpdatePhoneNumber(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetUpdatedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
        }

        public void ConfirmPhone()
        {
            PhoneNumberConfirmed = true;
        }

        public void UpdateLastLogin()
        {
            LastLoginDate = DateTime.UtcNow;
        }
    }
}
