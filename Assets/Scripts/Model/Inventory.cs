using System.Collections.Generic;
using DefaultNamespace.Configs;

namespace DefaultNamespace.Data
{
    public class Inventory
    {
        public List<(GrenadeConfig type, int amount)> Grenades = new List<(GrenadeConfig type, int amount)>();
        public int SelectedGrenade;
        
    }
}