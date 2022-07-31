using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Models;

namespace EmployeeApp.Controllers
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext;
        
        public EmployeeController(HRDatabaseContext context)
        {
            dbContext = context;
        }
        public IActionResult Index(String sortField , String curruntSortField , SortDirection sortDirection , String SearchByName)
        {
            var emps =  GetEmployees();
            if(!String.IsNullOrEmpty(SearchByName))
                emps = emps.Where(e => e.EmployeeName.ToLower().Contains(SearchByName.ToLower())).ToList();

            return View(this.SortEmployees(emps , sortField , curruntSortField , sortDirection));

        }

        private List<Employee> GetEmployees()
        {
            var employees = (from emp in dbContext.Employees
                             join Department in dbContext.Departments on emp.DepartmentId equals Department.DepartmentId
                             select new Employee
                             {
                                 EmployeeId = emp.EmployeeId,
                                 EmployeeName = emp.EmployeeName,
                                 EmployeeNumber = emp.EmployeeNumber,
                                 DOB = emp.DOB,
                                 HiringDate = emp.HiringDate,
                                 GrossSalary = emp.GrossSalary,
                                 NetSalary = emp.NetSalary,
                                 DepartmentId = emp.DepartmentId,
                                 DepartmentName = Department.DepartmentName,
                             }).ToList();

            return employees;
        }

        public IActionResult Create()
        {
            ViewBag.Departments = this.dbContext.Departments.ToList();

            return View();
        }
        [HttpPost ]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if(ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = this.dbContext.Departments.ToList();

            return View();

        }
        public IActionResult Edit(int id)
        {
            Employee employee= dbContext.Employees.Find(id);

            ViewBag.Departments = this.dbContext.Departments.ToList();

            return View("Create" , employee);   
        }
        [HttpPost]
        public IActionResult Edit(Employee em)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(em);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = this.dbContext.Departments.ToList();

            return View("Create", em);
        }
        public IActionResult Delete(int id)
        {
            Employee em= dbContext.Employees.Find(id);
            if(em != null)
            {
                dbContext.Employees.Remove(em);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public List<Employee> SortEmployees (List<Employee> employees , String sortField , String curruntSortField , SortDirection sortDirection)
        {
            if(String.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "EmployeeNumber";
                ViewBag.SortDirection = SortDirection.Ascending;
            }       
            else
            {
                if (sortField == curruntSortField)
                    ViewBag.SortDirection = sortDirection == SortDirection.Ascending  ? SortDirection.Descending : SortDirection.Ascending;
                else
                    ViewBag.SortDirection = SortDirection.Ascending;
                
                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(Employee).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == SortDirection.Ascending)
                employees = employees.OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
            else
                employees = employees.OrderByDescending(e => propertyInfo.GetValue(e , null)).ToList();

            return employees;
        }
    }
}
