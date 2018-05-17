using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Measure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDecimal { get; set; }
        static DAO.MeasureDAO DAO = new LaserArt.DAO.MeasureDAO();
        public static List<Measure> GetMeasure(int? id)
        {
            return DAO.getMeasure(id);
        }
    }
}