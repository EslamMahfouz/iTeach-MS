using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teach.EDM;

namespace Teach.BL
{
    class clsAdd
    {
        TeachEntities db = new TeachEntities();

        public void addAbsence(int idStudent)
        {
            tblAbsence abs = new tblAbsence()
            {
                idStudent = idStudent,
                date = DateTime.Now.Date,
                day1 = false,
                day2 = false,
                day3 = false,
                day4 = false,
                day5 = false,
                day6 = false,
                day7 = false,
                day8 = false,
                day9 = false,
                day10 = false,
                day11 = false,
                day12 = false,
                day13 = false,
                day14 = false,
                paid = false,
                exam = 0,
            };
            db.tblAbsences.Add(abs);
            db.SaveChanges();
        }
    }
}
