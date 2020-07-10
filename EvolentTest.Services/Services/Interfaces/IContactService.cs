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
        List<ContactModel> GetAllContacts(int page = 1, int pageSize = 10);
        Task<Contact> DeleteContact(Guid contactId);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneNumberExist(string phoneNumber);
    }
}