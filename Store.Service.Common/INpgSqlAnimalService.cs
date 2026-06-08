using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Model;
using Store.Common;


namespace Store.Service.Common
{
    public interface INpgSqlAnimalService
    {
        List<Animal> GetAllAnimals(AnimalFilter filter);
        Animal GetAnimalById(int id);
        Animal CreateAnimal(Animal animal);
        bool UpdateAnimal(int id, Animal animal);
        bool DeleteAnimal(int id);
    }
   
}
