using System.Collections.Generic;
using System.Reflection;

namespace ModernSystem.Factories
{
    public class ScoredConstructor
    {
        private const int ScoreUnusable = -1;
        private const int ScoreParameterless = 0;

        public ScoredConstructor(int score, ConstructorInfo constructor, IReadOnlyDictionary<int, int> map)
        {
            Score = score;
            Constructor = constructor;
            ArgumentMap = map ?? new Dictionary<int, int>();
        }

        public int Score { get; private set; }
        public ConstructorInfo Constructor { get; private set; }
        public IReadOnlyDictionary<int, int> ArgumentMap { get; private set; }

        public static ScoredConstructor Unusable(ConstructorInfo constructor)
        {
            return new ScoredConstructor(ScoreUnusable, constructor, null);
        }

        public static ScoredConstructor Parameterless(ConstructorInfo constructor)
        {
            return new ScoredConstructor(ScoreParameterless, constructor, null);
        }

        public object[] MapArgumentsToParameters(object[] args)
        {
            object[] parameters = new object[ArgumentMap.Count];
            for (int paramIdx = 0; paramIdx < parameters.Length; paramIdx++)
            {
                int argIdx = paramIdx;
                if (ArgumentMap.ContainsKey(paramIdx))
                    argIdx = ArgumentMap[paramIdx];

                parameters[paramIdx] = args[argIdx];
            }
            return parameters;
        }

        public object Construct(object[] args)
        {
            var mapped = MapArgumentsToParameters(args);

            return Constructor.Invoke(mapped);
        }
    }
}