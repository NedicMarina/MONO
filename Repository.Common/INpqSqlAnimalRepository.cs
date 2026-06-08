using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Common;

namespace Store.Repository.Common
{
    public interface INpgSqlAnimalRepository
    {

        List<Animal> GetAllAnimals(AnimalFilter filter);
        Animal GetAnimalById(int id);
        Animal CreateAnimal(Animal animal);
        bool UpdateAnimal(int id, Animal animal);
        bool DeleteAnimal(int id);
    }
}
