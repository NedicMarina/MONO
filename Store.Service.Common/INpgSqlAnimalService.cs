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
        Task<List<Animal>> GetAllAnimalsAsync(AnimalFilter filter);
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<Animal> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(int id, Animal animal);
        Task<bool> DeleteAnimalAsync(int id);
    }
   
}
