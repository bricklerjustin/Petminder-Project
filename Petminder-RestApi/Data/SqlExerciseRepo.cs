using System;
using System.Collections.Generic;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlExerciseRepo : IExerciseRepo
    {
        private readonly PetminderContext _context;

        public SqlExerciseRepo(PetminderContext context)
        {
            _context = context;
        }

        public IEnumerable<Exercises> GetAllPetExercises(Guid PetId, Guid AccountId)
        {
            return _context.Exercises.Where(p => p.PetId == PetId && p.AccountId == AccountId);
        }

        public IEnumerable<Exercises> GetAllExercises(Guid AccountId)
        {
            return _context.Exercises.Where(p => p.AccountId == AccountId);
        }

        public Exercises GetExerciseById(Guid id, Guid AccountId)
        {
            return _context.Exercises.Where(p => p.Id == id && p.AccountId == AccountId).Single();
        }

        public void CreateExercise(Exercises Exercise)
        {
            if (Exercise == null)
            {
                throw new ArgumentNullException(nameof(Exercise));
            }

            _context.Exercises.Add(Exercise);

        }

        public void UpdateExercise(Exercises Exercise)
        {
            //Nothing
        }

        public void DeleteExercise(Exercises Exercise)
        {
            if (Exercise == null)
            {
                throw new ArgumentNullException(nameof(Exercise));
            }

            _context.Exercises.Remove(Exercise);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
