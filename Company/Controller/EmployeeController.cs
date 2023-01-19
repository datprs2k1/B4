using Company.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Company.Controller
{
    internal class EmployeeController
    {
        CompanyEntities _context = new CompanyEntities();

        public List<Employee> getAll()
        {
            var list = _context.Employees.ToList();

            return list;
        }

        public Boolean add(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return true;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public Employee find(int id)
        {
            var employee = _context.Employees.Find(id);

            return employee;
        }

        public Boolean update(int id, Employee employee)
        {
            try
            {
                var a = _context.Employees.First(x => x.id == id);

                if (a == null) return false;

                a.name = employee.name;
                a.dob = employee.dob;
                a.gender = employee.gender;
                a.address = employee.address;
                a.startdate = employee.startdate;
                a.department_id = employee.department_id;
                a.salary_id = employee.salary_id;

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
                var a = _context.Employees.First(x => x.id == id);

                if (a == null) return false;

                _context.Employees.Remove(a);
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
