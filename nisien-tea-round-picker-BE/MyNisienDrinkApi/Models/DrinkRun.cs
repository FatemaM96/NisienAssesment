using System;
using System.Collections.Generic;

namespace MyNisienDrinkApi.Models
{
      public class DrinkRun
    {
        public string Id { get; set; }
        public User DrinkMaker { get; set; }
        public List<DrinkOrder> Orders { get; set; } = new();
    }


}
