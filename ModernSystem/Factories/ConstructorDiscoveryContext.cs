using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernSystem.Factories
{
    public class ConstructorDiscoveryContext
    {
        public List<Type> ArgumentTypes { get; private set; }
        public Type TargetType { get; private set; }

        public ConstructorDiscoveryContext(Type type, IEnumerable<Type> argumentTypes)
        {
            TargetType = type;

            ArgumentTypes = argumentTypes.ToList();
        }

        public Dictionary<Type, int> GetArgumentTypeCounts()
        {
            var counts = new Dictionary<Type, int>();
            foreach (var type in ArgumentTypes)
            {
                if (!counts.ContainsKey(type))
                    counts.Add(type, 0);
                counts[type]++;
            }
            return counts;
        }
    }
}