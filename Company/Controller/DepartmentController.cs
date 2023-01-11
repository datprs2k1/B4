using Company.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Controller
{
    internal class DepartmentController
    {
        CompanyEntities _context = new CompanyEntities();

        public List<Department> getAll()
        {
            var list = _context.Departments.ToList();

            return list;
        }

        public Boolean add(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Department find(int id)
        {
            var department = _context.Departments.Find(id);

            return department;
        }

        public Boolean update(Department department)
        {
            try
            {
                var a = _context.Departments.First(x => x.code == department.code);

                if (a == null) return false;
                a.code = department.code;
                a.name = department.name;
                a.description = department.description;
                a.parent = department.parent;

                _context.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public Boolean delete(int id)
        {
            try
            {
                var a = _context.Departments.First(x => x.id == id);

                if (a == null) return false;

                _context.Departments.Remove(a);
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
