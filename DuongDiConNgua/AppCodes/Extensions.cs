using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua
{
    public static class Extensions
    {
        public static bool IsEquals(this Point point, Point target)
        {
            return point.X == target.X && point.Y == target.Y;
        }
        public static void AddIgnore(this Dictionary<Point, List<Point>> dictionary, Point p, Point ignoredPoint)
        {
            try
            {
                var kv = dictionary.FirstOrDefault(x => x.Key.X == p.X && x.Key.Y == p.Y);
                kv.Value.Add(ignoredPoint);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{p.X} , {p.Y} is not found");
                dictionary.Add(p, new List<Point>() { ignoredPoint });
            }
        }
        public static void RemoveAllIfNotEquals(this Dictionary<Point, List<Point>> dictionary, Point p1, Point p2)
        {
            try
            {

                var keys = dictionary.Keys.Where(item => item.X != p1.X && item.Y != p1.Y && item.X != p2.X && item.Y != p2.Y);
                foreach (var item in keys)
                {
                    dictionary.Remove(item);
                }
                    
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
