﻿using EvolentTest.Database;
using EvolentTest.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolentTest.Common;

namespace EvolentTest.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactModel> AddContact(ContactModel contactModel)
        {
            return await _contactRepository.AddContact(contactModel);
        }

        public List<ContactModel> GetAllContacts(int page, int pageSize)
        {
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = 10;

            return _contactRepository.GetAllContacts(page, pageSize);
        }

        public async Task<ContactModel> UpdateContact(ContactModel contactModel)
        {
            return await _contactRepository.UpdateContact(contactModel);
        }

        public async Task<Contact> DeleteContact(Guid contactId)
        {
            return await _contactRepository.DeleteContact(contactId);
        }

        public async Task<bool> IsEmailExist(string userId, string email)
        {
            return await _contactRepository.IsEmailExist(userId, email);
        }

        public async Task<bool> IsPhoneNumberExist(string userId, string phoneNumber)
        {
            return await _contactRepository.IsPhoneNumberExist(userId, phoneNumber);
        }
    }
}
