using System;
using System.Collections.Generic;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlReminderRepo : IReminderRepo
    {
        private readonly PetminderContext _context;

        public SqlReminderRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateReminder(Reminders Reminder)
        {
            if (Reminder == null)
            {
                throw new ArgumentNullException(nameof(Reminder));
            }

            _context.Reminders.Add(Reminder);
        }

        public void DeleteReminder(Reminders Reminder)
        {
            if (Reminder == null)
            {
                throw new ArgumentNullException(nameof(Reminder));
            }

            _context.Reminders.Remove(Reminder);
        }

        public IEnumerable<Reminders> GetAllUserReminders(Guid AccountId)
        {
            return _context.Reminders.Where(p => p.AccountId == AccountId);
        }

        public Reminders GetReminderById(Guid id, Guid AccountId)
        {
            return _context.Reminders.FirstOrDefault(p => p.AccountId == AccountId && p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateReminder(Reminders Reminder)
        {
            //Nothing
        }
    }
}