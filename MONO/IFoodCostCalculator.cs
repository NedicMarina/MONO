using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO
{
   public interface IFoodCostCalculator
    {
        double CalculateDailyFoodCost();
        double CalculateMonthlyFoodCost();
    }
}
