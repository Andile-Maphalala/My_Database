using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Project.ViewModels
{
    public class EmployeeViewModel
    {
        public int mID { get; set; }
        public string mName { get; set; }
        public string mSurname { get; set; }
        public double mSalary { get; set; }
        public int mAge { get; set; }

        public EmployeeViewModel(int ID, string Name, string Surname, double Salary, int Age)
        {
            mID = ID;
            mName = Name;
            mSurname = Surname;
            mSalary = Salary;
            mAge = Age;
        }

        public EmployeeViewModel()
        {
            mID = 0;
            mName = "";
            mSurname = "";
            mSalary = 0;
            mAge = 0;
        }
    }
}