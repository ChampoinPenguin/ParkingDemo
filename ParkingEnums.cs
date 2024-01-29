using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDemo
{
    public static class EnumExtension
    {
        public static string GetDescriptionText<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
    public enum Vehicle
    {
        [Description("未知")]Unknown,
        [Description("機車")]Scooter,
        [Description("汽車")]Car,
        [Description("電動車")]ElectricCar,
        [Description("身障專用")]ForDisability,
        [Description("婦幼專用")]ForChindern,
        [Description("大型車")]Bus,
    }

}
