using Company.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Company.Controller
{
    internal class SalaryController
    {
        CompanyEntities _context = new CompanyEntities();

        public List<Salary> getAll()
        {
            var list = _context.Salaries.ToList();

            return list;
        }

        public Boolean add(Salary salary)
        {
            try
            {
                _context.Salaries.Add(salary);
                _context.SaveChanges();

                return true;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public Salary find(int id)
        {
            var salary = _context.Salaries.Find(id);

            return salary;
        }

        public Boolean update(int id, Salary salary)
        {
            try
            {
                var a = _context.Salaries.First(x => x.id == id);

                if (a == null) return false;
                a.name = salary.name;
                a.coefficient = salary.coefficient;

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
                var a = _context.Salaries.First(x => x.id == id);

                if (a == null) return false;

                _context.Salaries.Remove(a);
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
