using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        void AddDog(Dog dog);
        void DeleteDog(int id);
        void UpdateDog(Dog dog);
        Dog GetDogById(int id);
    }
}