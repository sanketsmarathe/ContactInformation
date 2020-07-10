using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolentTest.Common;
using EvolentTest.Database.Entities;

namespace EvolentTest.Services
{
    public interface IContactRepository
    {
        Task<ContactModel> AddContact(ContactModel contact);
        Task<ContactModel> UpdateContact(ContactModel contactModel);
        List<ContactModel> GetAllContacts(int page = 1, int pageSize = 10);
        Task<Contact> DeleteContact(Guid contactId);
        Task<Contact> GetContactByContactId(Guid contactId);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneNumberExist(string phoneNumber);
    }
}