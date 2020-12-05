using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IExerciseRepo
    {
        bool SaveChanges();

        IEnumerable<Exercises> GetAllExercises(Guid AccountId);
        IEnumerable<Exercises> GetAllPetExercises(Guid PetId, Guid AccountId);
        Exercises GetExerciseById(Guid id, Guid AccountId);
        void CreateExercise(Exercises Exercise);
        void UpdateExercise(Exercises Exercise);
        void DeleteExercise(Exercises Exercise);
    }
}