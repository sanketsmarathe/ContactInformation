using EvolentTest.Database;
using EvolentTest.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolentTest.Common;
using EvolentTest.Common.Constants;

namespace EvolentTest.Services
{
    public class ContactRepository : IContactRepository
    {
        private EvolentTestDBContext _context;

        public ContactRepository(EvolentTestDBContext context)
        {
            _context = context;
        }

        public async Task<ContactModel> AddContact(ContactModel contactModel)
        {
            var applicationUser = await AddApplicationUser(contactModel);

            Contact contact = new Contact()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse(applicationUser.Id),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.Parse(applicationUser.Id),
                Status = ContactConstant.ActiveStatus,
                Id = applicationUser.Id
            };

            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            contactModel.ContactId = contact.ContactId;
            contactModel.UserId = contact.Id;

            return contactModel;
        }

        public List<ContactModel> GetAllContacts(int page, int pageSize)
        {
            try
            {
                var contactList = _context.Contact
                                  .Join(_context.User,
                                        con => con.Id,
                                        user => user.Id,
                                        (con, user) => new { CON = con, USER = user })
                                  .Where(x => x.CON.Status.Equals(ContactConstant.ActiveStatus))
                                  .OrderByDescending(x => x.CON.UpdatedAt)
                                  .Select(x => new ContactModel()
                                  {
                                      ContactId = x.CON.ContactId,
                                      Email = x.USER.Email,
                                      FirstName = x.USER.FirstName,
                                      LastName = x.USER.LastName,
                                      PhoneNumber = x.USER.PhoneNumber,
                                      Status = x.CON.Status,
                                      UserId = x.USER.Id
                                  }).GetPaged(page, pageSize).Results.ToList();

                return contactList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContactModel> UpdateContact(ContactModel contactModel)
        {
            var contact = await GetContactByContactId(contactModel.ContactId);

            if (contact == null)
            {
                throw new Exception(MessageConstant.CONTACT_NOT_FOUND);
            }

            contactModel.UserId = contact.Id;

            if (await IsEmailExist(contactModel.UserId, contactModel.Email))
            {
                throw new Exception(MessageConstant.EMAIL_ALREADY_EXIST);
            }

            if (await IsPhoneNumberExist(contactModel.UserId, contactModel.PhoneNumber))
            {
                throw new Exception(MessageConstant.PHONE_ALREADY_EXIST);
            }

            await UpdateApplicationUser(contactModel);

            contact.Status = !string.IsNullOrEmpty(contactModel.Status) ? contactModel.Status : contact.Status;

            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return contactModel;
        }

        public async Task<Contact> DeleteContact(Guid contactId)
        {
            var contact = await GetContactByContactId(contactId);

            if (contact == null)
            {
                throw new Exception(MessageConstant.CONTACT_NOT_FOUND);
            }

            contact.Status = ContactConstant.InActiveStatus;
            contact.UpdatedAt = DateTime.UtcNow;

            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> GetContactByContactId(Guid contactId)
        {
            return await _context.Contact.FindAsync(contactId);
        }

        public async Task<bool> IsEmailExist(string userId, string email)
        {
            return await _context.User.AnyAsync(x => x.Email.Equals(email) && x.Id != userId);
        }

        public async Task<bool> IsPhoneNumberExist(string userId, string phoneNumber)
        {
            return await _context.User.AnyAsync(x => x.PhoneNumber.Equals(phoneNumber) && x.Id != userId);
        }


        #region Private Methods

        private async Task<ApplicationUser> AddApplicationUser(ContactModel contact)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                UserName = contact.FirstName.ToLower() + contact.FirstName.ToLower(),
                Email = contact.Email,
                PasswordHash = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };

            await _context.User.AddAsync(applicationUser);
            await _context.SaveChangesAsync();

            return applicationUser;
        }

        private async Task<ApplicationUser> GetApplicationUser(string userId)
        {
            return await _context.User.FindAsync(userId);
        }

        private async Task<ApplicationUser> UpdateApplicationUser(ContactModel contact)
        {
            var appUser = await GetApplicationUser(contact.UserId);

            if (appUser == null)
            {
                throw new Exception(MessageConstant.USER_NOT_FOUND);
            }

            appUser.FirstName = !string.IsNullOrEmpty(contact.FirstName) ? contact.FirstName : appUser.FirstName;
            appUser.LastName = !string.IsNullOrEmpty(contact.LastName) ? contact.LastName : appUser.LastName;
            appUser.Email = !string.IsNullOrEmpty(contact.Email) ? contact.Email : appUser.Email;
            appUser.PhoneNumber = !string.IsNullOrEmpty(contact.PhoneNumber) ? contact.PhoneNumber : appUser.PhoneNumber;

            _context.Entry(appUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return appUser;
        }



        #endregion
    }
}
