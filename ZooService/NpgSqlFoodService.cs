using Store.Model;
using Store.Repository.Common;
using Store.Service.Common;
using Store.Repository;
using Store.Common;

namespace Store.Service
{
    public class NpgSqlFoodService : INpgSqlFoodService
    {
        private readonly NpgSqlFoodRepository foodRepository = new NpgSqlFoodRepository();

      
        public List<Food> GetAllFoods(FoodFilter filter)
        {
            return foodRepository.GetAllFoods(filter);
        }

        public Food GetFoodById(int id)
        {
            return foodRepository.GetFoodById(id);
        }

        public Food CreateFood(Food food)
        {
            return foodRepository.CreateFood(food);
        }

        public bool UpdateFood(int id, Food food)
        {
            return foodRepository.UpdateFood(id, food);
        }

        public bool DeleteFood(int id)
        {
            return foodRepository.DeleteFood(id);
        }
    }
}