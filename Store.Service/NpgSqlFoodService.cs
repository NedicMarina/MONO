using Store.Model;
using Store.Repository.Common;
using Store.Service.Common;
using Store.Repository;
using Store.Common;
using System.Threading.Tasks;

namespace Store.Service
{
    public class NpgSqlFoodService : INpgSqlFoodService
    {
        private readonly NpgSqlFoodRepository foodRepository = new NpgSqlFoodRepository();

      
        public async Task<List<Food>> GetAllFoodsAsync(FoodFilter filter)
        {
            return await foodRepository.GetAllFoodsAsync(filter);
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            return await foodRepository.GetFoodByIdAsync(id);
        }

        public async Task<Food> CreateFoodAsync(Food food)
        {
            return await foodRepository.CreateFoodAsync(food);
        }

        public async Task<bool> UpdateFoodAsync(int id, Food food)
        {
            return await foodRepository.UpdateFoodAsync(id, food);
        }

        public async Task<bool> DeleteFoodAsync(int id)
        {
            return await foodRepository.DeleteFoodAsync(id);
        }
    }
}