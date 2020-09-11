using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CostAnalysis.Models
{
    public class Day : IEquatable<Day>, IComparable, IComparable<DateTime>
    {
        private DateTime _date;
        private double _totalSum;
        private double _alchoSum;
        private double _entertainmentSum;
        private double _foodSum;
        private double _householdSum;

        [Key]
        public int Id { get; set; }
        public DateTime Date
        {
            get => new DateTime(_date.Year, _date.Month, _date.Day);
            set
            {
                _date = new DateTime(value.Year, value.Month, value.Day);
            }
        }
        public double Total
        {
            get => Math.Round(_totalSum, 2);
            set => _totalSum = value;
        }
        public double Alcohol
        {
            get => Math.Round(_alchoSum, 2);
            set => _alchoSum = value;
        }
        public double Entertainments
        {
            get => Math.Round(_entertainmentSum, 2);
            set => _entertainmentSum = value;
        }
        public double Food
        {
            get => Math.Round(_foodSum, 2);
            set => _foodSum = value;
        }
        public double Household
        {
            get
            {
                return Math.Round(_householdSum, 2);
            }
            set => _householdSum = value;
        }
        public List<KeyValuePair<string, double>> Shops { get; set; }

        public Day()
        { }

        public Day(DateTime date, double total, double alcohol, double entertainments, double food, double houshold, List<KeyValuePair<string, double>> shops)
        {
            Date = date;
            Total = total;
            Alcohol = alcohol;
            Entertainments = entertainments;
            Food = food;
            Household = houshold;
            Shops = shops;
        }

        public Day(double total)
        {
            Total = total;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Day);
        }

        public bool Equals(Day other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Date.CostDateEquals(other.Date) &&
                   this.Total == other.Total &&
                   this.Alcohol == other.Alcohol &&
                   this.Entertainments == other.Entertainments &&
                   this.Food == other.Food &&
                   this.Household == other.Household;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt16(Total * Alcohol * Entertainments) ^ Id;
        }

        public int CompareTo(object obj)
        {
            if (this.Equals(obj as Day))
            {
                return 1;
            }
            return 0;
        }

        public int CompareTo([AllowNull] DateTime other)
        {
            if (this.Equals(other))
            {
                return 1;
            }
            return 0;
        }
    }
}