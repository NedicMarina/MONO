using Store.Common;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Common
{
    public interface INpgSqlFoodService
    {
       
        List<Food> GetAllFoods(FoodFilter filter);
        Food GetFoodById(int id);
        Food CreateFood(Food food);
        bool UpdateFood(int id, Food food);
        bool DeleteFood(int id);

    }
}
