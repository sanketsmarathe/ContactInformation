using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentTest.Common;
using EvolentTest.Database.Entities;

namespace EvolentTest.Services
{
    public interface IContactService
    {
        Task<ContactModel> AddContact(ContactModel contact);
        Task<ContactModel> UpdateContact(ContactModel contact);
        List<ContactModel> GetAllContacts(int page, int pageSize);
        Task<Contact> DeleteContact(Guid contactId);
        Task<bool> IsEmailExist(string userId, string email);
        Task<bool> IsPhoneNumberExist(string userId, string phoneNumber);
    }
}