using Company.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Controller
{
    public class AttendanceController
    {
        CompanyEntities _context = new CompanyEntities();

        public List<Attendance> getAll()
        {
            var list = _context.Attendances.ToList();

            return list;
        }

        public Boolean add(Attendance attendance)
        {
            try
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Attendance find(int id)
        {
            var attendance = _context.Attendances.Find(id);

            return attendance;
        }

        public Boolean update(int id, Attendance attendance)
        {
            try
            {
                var a = _context.Attendances.First(x => x.id == id);

                if (a == null) return false;
                a.employee_id = attendance.employee_id;
                a.status = attendance.status;
                a.created_at = attendance.created_at;

                _context.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
