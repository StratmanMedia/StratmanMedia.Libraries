using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace StratmanMedia.GuardClauses
{
    public static class GuardClauseExtensions
    {
        public static T Null<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null)
        {
            if (input is null)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(parameterName);
                }
                throw new ArgumentNullException(message, (Exception?)null);
            }

            return input;
        }

        public static string NullOrEmpty(this IGuardClause guardClause, string? input, string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName);
            if (input == string.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static Guid NullOrEmpty(this IGuardClause guardClause, Guid? input, string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName);
            if (input == Guid.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input.Value;
        }

        public static IEnumerable<T> NullOrEmpty<T>(this IGuardClause guardClause, IEnumerable<T>? input, string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static string NullOrWhiteSpace(this IGuardClause guardClause, string? input, string parameterName, string? message = null)
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static int OutOfRange(this IGuardClause guardClause, int input, string parameterName, int rangeFrom, int rangeTo, string? message = null)
        {
            return OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static DateTime OutOfRange(this IGuardClause guardClause, DateTime input, string parameterName, DateTime rangeFrom, DateTime rangeTo, string? message = null)
        {
            return OutOfRange<DateTime>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static DateTime OutOfSQLDateRange(this IGuardClause guardClause, DateTime input, string parameterName, string? message = null)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            return OutOfRange<DateTime>(guardClause, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks), message);
        }

        public static decimal OutOfRange(this IGuardClause guardClause, decimal input, string parameterName, decimal rangeFrom, decimal rangeTo, string? message = null)
        {
            return OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static short OutOfRange(this IGuardClause guardClause, short input, string parameterName, short rangeFrom, short rangeTo, string? message = null)
        {
            return OutOfRange<short>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static double OutOfRange(this IGuardClause guardClause, double input, string parameterName, double rangeFrom, double rangeTo, string? message = null)
        {
            return OutOfRange<double>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static float OutOfRange(this IGuardClause guardClause, float input, string parameterName, float rangeFrom, float rangeTo, string? message = null)
        {
            return OutOfRange<float>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static TimeSpan OutOfRange(this IGuardClause guardClause, TimeSpan input, string parameterName, TimeSpan rangeFrom, TimeSpan rangeTo, string? message = null)
        {
            return OutOfRange<TimeSpan>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        private static T OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom, T rangeTo, string? message = null)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException(message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range");
                }
                throw new ArgumentOutOfRangeException(message, (Exception?)null);
            }

            return input;
        }

        public static int Zero(this IGuardClause guardClause, int input, string parameterName, string? message = null)
        {
            return Zero<int>(guardClause, input, parameterName, message);
        }

        public static long Zero(this IGuardClause guardClause, long input, string parameterName, string? message = null)
        {
            return Zero<long>(guardClause, input, parameterName, message);
        }

        public static decimal Zero(this IGuardClause guardClause, decimal input, string parameterName, string? message = null)
        {
            return Zero<decimal>(guardClause, input, parameterName, message);
        }

        public static float Zero(this IGuardClause guardClause, float input, string parameterName, string? message = null)
        {
            return Zero<float>(guardClause, input, parameterName, message);
        }

        public static double Zero(this IGuardClause guardClause, double input, string parameterName, string? message = null)
        {
            return Zero<double>(guardClause, input, parameterName, message);
        }

        public static TimeSpan Zero(this IGuardClause guardClause, TimeSpan input, string parameterName)
        {
            return Zero<TimeSpan>(guardClause, input, parameterName);
        }

        private static T Zero<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero.", parameterName);
            }

            return input;
        }

        public static int Negative(this IGuardClause guardClause, int input, string parameterName, string? message = null)
        {
            return Negative<int>(guardClause, input, parameterName, message);
        }

        public static long Negative(this IGuardClause guardClause, long input, string parameterName, string? message = null)
        {
            return Negative<long>(guardClause, input, parameterName, message);
        }

        public static decimal Negative(this IGuardClause guardClause, decimal input, string parameterName, string? message = null)
        {
            return Negative<decimal>(guardClause, input, parameterName, message);
        }

        public static float Negative(this IGuardClause guardClause, float input, string parameterName, string? message = null)
        {
            return Negative<float>(guardClause, input, parameterName, message);
        }

        public static double Negative(this IGuardClause guardClause, double input, string parameterName, string? message = null)
        {
            return Negative<double>(guardClause, input, parameterName, message);
        }

        public static TimeSpan Negative(this IGuardClause guardClause, TimeSpan input, string parameterName, string? message = null)
        {
            return Negative<TimeSpan>(guardClause, input, parameterName, message);
        }

        private static T Negative<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be negative.", parameterName);
            }

            return input;
        }

        public static int NegativeOrZero(this IGuardClause guardClause, int input, string parameterName, string? message = null)
        {
            return NegativeOrZero<int>(guardClause, input, parameterName, message);
        }

        public static long NegativeOrZero(this IGuardClause guardClause, long input, string parameterName, string? message = null)
        {
            return NegativeOrZero<long>(guardClause, input, parameterName, message);
        }

        public static decimal NegativeOrZero(this IGuardClause guardClause, decimal input, string parameterName, string? message = null)
        {
            return NegativeOrZero<decimal>(guardClause, input, parameterName, message);
        }

        public static float NegativeOrZero(this IGuardClause guardClause, float input, string parameterName, string? message = null)
        {
            return NegativeOrZero<float>(guardClause, input, parameterName, message);
        }

        public static double NegativeOrZero(this IGuardClause guardClause, double input, string parameterName, string? message = null)
        {
            return NegativeOrZero<double>(guardClause, input, parameterName, message);
        }

        public static TimeSpan NegativeOrZero(this IGuardClause guardClause, TimeSpan input, string parameterName, string? message = null)
        {
            return NegativeOrZero<TimeSpan>(guardClause, input, parameterName, message);
        }

        private static T NegativeOrZero<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero or negative.", parameterName);
            }

            return input;
        }

        public static int OutOfRange<T>(this IGuardClause guardClause, int input, string parameterName, string? message = null) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
                }
                throw new InvalidEnumArgumentException(message);
            }

            return input;
        }

        public static T OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
                }
                throw new InvalidEnumArgumentException(message);
            }

            return input;
        }

        public static T Default<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
            {
                throw new ArgumentException(message ?? $"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
            }

            return input;
        }

        public static string InvalidFormat(this IGuardClause guardClause, string input, string parameterName, string regexPattern, string? message = null)
        {
            if (input != Regex.Match(input, regexPattern).Value)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} was not in required format", parameterName);
            }

            return input;
        }

        public static T InvalidInput<T>(this IGuardClause guardClause, T input, string parameterName, Func<T, bool> predicate, string? message = null)
        {
            if (!predicate(input))
            {
                throw new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
            }

            return input;
        }


        private static IEnumerable<T> OutOfRange<T>(this IGuardClause guardClause, IEnumerable<T> input, string parameterName, T rangeFrom, T rangeTo, string? message = null) where T : IComparable
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException(message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (input.Any(x => comparer.Compare(x, rangeFrom) < 0 || comparer.Compare(x, rangeTo) > 0))
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Input {parameterName} had out of range item(s)");
                }
                throw new ArgumentOutOfRangeException(message, (Exception?)null);
            }

            return input;
        }

        public static IEnumerable<int> OutOfRange(this IGuardClause guardClause, IEnumerable<int> input, string parameterName, int rangeFrom, int rangeTo, string? message = null)
        {
            return OutOfRange<int>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<long> OutOfRange(this IGuardClause guardClause, IEnumerable<long> input, string parameterName, long rangeFrom, long rangeTo, string? message = null)
        {
            return OutOfRange<long>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<decimal> OutOfRange(this IGuardClause guardClause, IEnumerable<decimal> input, string parameterName, decimal rangeFrom, decimal rangeTo, string? message = null)
        {
            return OutOfRange<decimal>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<float> OutOfRange(this IGuardClause guardClause, IEnumerable<float> input, string parameterName, float rangeFrom, float rangeTo, string? message = null)
        {
            return OutOfRange<float>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<double> OutOfRange(this IGuardClause guardClause, IEnumerable<double> input, string parameterName, double rangeFrom, double rangeTo, string? message = null)
        {
            return OutOfRange<double>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<short> OutOfRange(this IGuardClause guardClause, IEnumerable<short> input, string parameterName, short rangeFrom, short rangeTo, string? message = null)
        {
            return OutOfRange<short>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static IEnumerable<TimeSpan> OutOfRange(this IGuardClause guardClause, IEnumerable<TimeSpan> input, string parameterName, TimeSpan rangeFrom, TimeSpan rangeTo, string? message = null)
        {
            return OutOfRange<TimeSpan>(guardClause, input, parameterName, rangeFrom, rangeTo, message);
        }

        public static T NotFound<T>(this IGuardClause guardClause, string key, T input, string parameterName)
        {
            guardClause.NullOrEmpty(key, nameof(key));

            if (input is null)
            {
                throw new NotFoundException(key, parameterName);
            }

            return input;
        }

        public static T NotFound<TKey, T>(this IGuardClause guardClause, TKey key, T input, string parameterName) where TKey : struct
        {
            guardClause.Null(key, nameof(key));

            if (input is null)
            {
                // TODO: Can we safely consider that ToString() won't return null for struct?
                throw new NotFoundException(key.ToString()!, parameterName);
            }

            return input;
        }

    }
}