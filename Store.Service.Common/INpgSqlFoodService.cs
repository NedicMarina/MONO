using Store.Common;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Common
{
    public interface IFoodService
    {
       
        Task<List<Food>> GetAllFoodsAsync(FoodFilter filter);
        Task<Food> CreateFoodAsync(Food food);
        Task<Food> GetFoodByIdAsync(int id);
        Task<bool> UpdateFoodAsync(int id, Food food);
        Task<bool> DeleteFoodAsync(int id);

    }
}
