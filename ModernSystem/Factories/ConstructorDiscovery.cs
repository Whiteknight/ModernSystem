using ModernSystem.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModernSystem.Factories
{
    public static class ConstructorDiscovery
    {
        private const int ExactTypeMatch = 2;
        private const int AssignableTypeMatch = 1;

        public static Func<T> CreateFactoryMethod<T>()
        {
            var constructor = GetBestConstructor(typeof(T), new Type[0]);
            if (constructor == null)
                return null;

            return Expression
                .Lambda<Func<T>>(
                    Expression.New(constructor.Constructor)
                )
                .Compile();
        }

        public static Func<TArg1, T> CreateFactoryMethod<TArg1, T>()
        {
            var constructor = GetBestConstructor(typeof(T), new[] { typeof(TArg1) });
            if (constructor == null)
                return null;

            var parameters = new ParameterExpression[] {
                Expression.Parameter(typeof(TArg1), "arg1")
            };

            return Expression
                .Lambda<Func<TArg1, T>>(
                    Expression.New(constructor.Constructor, parameters.Cast<Expression>()), parameters
                )
                .Compile();
        }

        public static Func<TArg1, TArg2, T> CreateFactoryMethod<TArg1, TArg2, T>()
        {
            var constructor = GetBestConstructor(typeof(T), new[] { typeof(TArg1), typeof(TArg2) });
            if (constructor == null)
                return null;

            var parameters = new ParameterExpression[] {
                Expression.Parameter(typeof(TArg1), "arg1"),
                Expression.Parameter(typeof(TArg2), "arg2")
            };

            return Expression
                .Lambda<Func<TArg1, TArg2, T>>(
                    Expression.New(constructor.Constructor, parameters.Cast<Expression>()), parameters
                )
                .Compile();
        }

        public static Func<TArg1, TArg2, TArg3, T> CreateFactoryMethod<TArg1, TArg2, TArg3, T>()
        {
            var constructor = GetBestConstructor(typeof(T), new[] { typeof(TArg1), typeof(TArg2), typeof(TArg3) });
            if (constructor == null)
                return null;

            var parameters = new ParameterExpression[] {
                Expression.Parameter(typeof(TArg1), "arg1"),
                Expression.Parameter(typeof(TArg2), "arg2"),
                Expression.Parameter(typeof(TArg3), "arg3")
            };

            return Expression
                .Lambda<Func<TArg1, TArg2, TArg3, T>>(
                    Expression.New(constructor.Constructor, parameters.Cast<Expression>()), parameters
                )
                .Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, T> CreateFactoryMethod<TArg1, TArg2, TArg3, TArg4, T>()
        {
            var constructor = GetBestConstructor(typeof(T), new[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4) });
            if (constructor == null)
                return null;

            var parameters = new ParameterExpression[] {
                Expression.Parameter(typeof(TArg1), "arg1"),
                Expression.Parameter(typeof(TArg2), "arg2"),
                Expression.Parameter(typeof(TArg3), "arg3"),
                Expression.Parameter(typeof(TArg4), "arg4")
            };

            return Expression
                .Lambda<Func<TArg1, TArg2, TArg3, TArg4, T>>(
                    Expression.New(constructor.Constructor, parameters.Cast<Expression>()), parameters
                )
                .Compile();
        }

        public static T CreateInstance<T>(params object[] args)
            where T : class
        {
            return CreateInstance(typeof(T), args) as T;
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            var constructor = GetBestConstructor(type, args.OrEmptyIfNull().Select(a => a.GetType()));
            if (constructor == null)
                return null;

            return constructor.Construct(args);
        }

        public static ScoredConstructor GetBestConstructor(Type type, IEnumerable<Type> argumentTypes)
        {
            var context = new ConstructorDiscoveryContext(type, argumentTypes);
            var scored = GetConstructorsByScore(context);
            return scored.Where(sc => sc.Score >= 0).OrderByDescending(sc => sc.Score).FirstOrDefault();
        }

        private static List<ScoredConstructor> GetConstructorsByScore(ConstructorDiscoveryContext context)
        {
            var constructors = context.TargetType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            var scoredList = new List<ScoredConstructor>();
            foreach (var constructor in constructors)
            {
                ScoredConstructor scored = ScoreConstructor(constructor, context);
                scoredList.Add(scored);
            }
            return scoredList;
        }

        private static ScoredConstructor ScoreConstructor(ConstructorInfo constructor, ConstructorDiscoveryContext context)
        {
            if (constructor.IsPrivate)
                return ScoredConstructor.Unusable(constructor);

            // parameters are the defined formal parameters of the constructor
            // arguments are the values and object references provided by the caller
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
                return ScoredConstructor.Parameterless(constructor);

            int score = 0;
            List<bool> usedArguments = context.ArgumentTypes.Select(t => false).ToList();
            var argumentMap = new Dictionary<int, int>();
            for (int paramIdx = 0; paramIdx < parameters.Length; paramIdx++)
            {
                var parameter = parameters[paramIdx];
                int foundScore = -1;
                var parameterType = parameter.ParameterType;
                for (int argIdx = 0; argIdx < context.ArgumentTypes.Count; argIdx++)
                {
                    if (usedArguments[argIdx])
                        continue;
                    var argType = context.ArgumentTypes[argIdx];
                    foundScore = ScoreTypeMatch(argType, parameterType);
                    if (foundScore >= 0)
                    {
                        usedArguments[argIdx] = true;
                        argumentMap.Add(paramIdx, argIdx);
                        score += foundScore;
                        break;
                    }
                }
                if (foundScore < 0)
                    return ScoredConstructor.Unusable(constructor);
            }

            return new ScoredConstructor(score, constructor, argumentMap);
        }

        private static int ScoreTypeMatch(Type argType, Type parameterType)
        {
            if (argType == parameterType)
                return ExactTypeMatch;
            if (parameterType.IsAssignableFrom(argType))
                return AssignableTypeMatch;
            return -1;
        }
    }
}