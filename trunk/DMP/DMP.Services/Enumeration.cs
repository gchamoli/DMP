using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMP.Services {

    public static class Enumeration {

        public static IEnumerable<KeyValuePair<int, string>> GetAll<TEnum>() where TEnum : struct {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new ArgumentException("Enumeration type is expected");
            var list = (from int value in Enum.GetValues(enumType) let name = Enum.GetName(enumType, value) select new KeyValuePair<int, string>(value, name)).ToList();
            return list;
        }

    }

    public enum Designation {
        Dse = 0,
        Dsm
    }

    public enum TrainingLevel {
        A = 0,
        B, C
    }

    public enum Months {
        January = 1,
        February, March, April, May, June, July, August, Sepetember, October, November, December
    }
}
