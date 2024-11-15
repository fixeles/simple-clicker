using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Inventory
    {
        public event Action<string, CountChangeData> UpdateEvent;
        private readonly Dictionary<string, double> _container = new();


        public IEnumerable<KeyValuePair<string, double>> AllItems => _container.ToArray();

        public void AddItem(string id, double count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();

            _container.TryAdd(id, 0);
            _container[id] += count;
            
            UpdateEvent?.Invoke(id, new CountChangeData
            {
                Difference = count,
                Total = _container[id]
            });
        }

        public bool TrySubtractItem(string id, double count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();

            if (!_container.ContainsKey(id))
                return false;

            if (_container[id] - count < 0)
                return false;

            _container[id] -= count;
            UpdateEvent?.Invoke(id, new CountChangeData
            {
                Difference = -count,
                Total = _container[id]
            });
            return true;
        }

        public struct CountChangeData
        {
            public double Total;
            public double Difference;
        }
    }
}