using System;
using Store.Common;
using Store.Model;

namespace Store.Repository.Common
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetAllFoodsAsync(FoodFilter filter);
        Task<Food> GetFoodByIdAsync(int id);
        Task<Food> CreateFoodAsync(Food food);
        Task<bool> UpdateFoodAsync(int id, Food food);
        Task<bool> DeleteFoodAsync(int id);
    }
}