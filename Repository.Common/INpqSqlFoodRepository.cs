using System;
using Store.Common;
using Store.Model;

namespace Store.Repository.Common
{
    public interface INpgSqlFoodRepository
    {
        List<Food> GetAllFoods(FoodFilter filter);
        Food GetFoodById(int id);
        Food CreateFood(Food food);
        bool UpdateFood(int id, Food food);
        bool DeleteFood(int id);
    }
}