

using Store.Model;
using Store.Repository;
using Store.Repository.Common;
using Store.Service.Common;
using Store.Common;


namespace Store.Service
{
    public class NpgSqlAnimalService : INpgSqlAnimalService
    {
        private readonly NpgSqlAnimalRepository animalRepository = new NpgSqlAnimalRepository();

       

        public List<Animal> GetAllAnimals(AnimalFilter filter)
        {
            return animalRepository.GetAllAnimals(filter);
        }

        public Animal GetAnimalById(int id)
        {
            return animalRepository.GetAnimalById(id);
        }

        public Animal CreateAnimal(Animal animal)
        {
            return animalRepository.CreateAnimal(animal);
        }

        public bool UpdateAnimal(int id, Animal animal)
        {
            return animalRepository.UpdateAnimal(id, animal);
        }

        public bool DeleteAnimal(int id)
        {
            return animalRepository.DeleteAnimal(id);
        }
    }
}
