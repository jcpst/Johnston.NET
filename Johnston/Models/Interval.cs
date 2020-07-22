using System;
using System.Numerics;
using Rationals;

namespace Johnston.Models
{
    public class Interval : IEquatable<Interval> {
        public Rational Ratio { get; private set; }
        public double Cents { get; private set; }

        #region Initialization
        
        public Interval(int num)
        {
            Init(new Rational(num));
        }
        
        public Interval(int num, int den)
        {
            Init(new Rational(num, den));
        }

        private Interval(BigInteger num, BigInteger den)
        {
            Init(new Rational(num, den));
        }

        private Interval(Rational ratio)
        {
            Init(ratio);
        }

        private void Init(Rational ratio)
        {
            Ratio = Flatten(ratio);
            Cents = CalcCents(Ratio);
        }
        
        #endregion
        
        public bool Equals(Interval other) => other != null && Ratio == other.Ratio;
        
        public int GetHashCode(Interval interval) => interval.Ratio.GetHashCode();

        public static Interval operator !(Interval a) => new Interval(a.Ratio.Denominator, a.Ratio.Numerator);
 
        public static Interval operator +(Interval a, Interval b) => new Interval(a.Ratio * b.Ratio);
        
        public static Interval operator -(Interval a, Interval b) => new Interval(a.Ratio / b.Ratio);

        private static double CalcCents(Rational ratio)
        {
            var numerator = (double) ratio.Numerator; 
            var denominator = (double) ratio.Denominator;
            return 1200 / Math.Log10(2) * Math.Log10(numerator / denominator);
        }

        private static Rational Flatten(Rational ratio)
        {
            while (true)
            {
                if (ratio > 2)
                    ratio /= 2;
                else if (ratio < 1)
                    ratio *= 2;
                else
                    return ratio.CanonicalForm;
            }
        }
    }
}