using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IReminderRepo
    {
        bool SaveChanges();
        // bool ValidateAccountKey(Guid AccountId);

        IEnumerable<Reminders> GetAllUserReminders(Guid AccountId);
        Reminders GetReminderById(Guid id, Guid AccountId);
        void CreateReminder(Reminders Reminder);
        void UpdateReminder(Reminders Reminder);
        void DeleteReminder(Reminders Reminder); 
    }
}