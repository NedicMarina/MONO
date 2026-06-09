

using Store.Model;
using Store.Repository;
using Store.Repository.Common;
using Store.Service.Common;
using Store.Common;


namespace Store.Service
{
    public class AnimalService : IAnimalService
    {
        // private readonly AnimalRepository animalRepository = new AnimalRepository();

        private readonly IAnimalRepository animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public async Task<List<Animal>> GetAllAnimalsAsync(AnimalFilter filter)
        {
            return await animalRepository.GetAllAnimalsAsync(filter);
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await animalRepository.GetAnimalByIdAsync(id);
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            return await animalRepository.CreateAnimalAsync(animal);
        }

        public async Task<bool> UpdateAnimalAsync(int id, Animal animal)
        {
            return await animalRepository.UpdateAnimalAsync(id, animal);
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            return await animalRepository.DeleteAnimalAsync(id);
        }
    }
}
