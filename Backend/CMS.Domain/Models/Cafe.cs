using CMS.Domain.Abstractions;
using CMS.Domain.Exceptions;
using CMS.Domain.ValueObjects;

namespace CMS.Domain.Models
{
    public class Cafe:Entity<CafeId>
    {
        private readonly List<Employee> _employees = [];
        
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string? Logo { get; private set; }
        public string Location { get; private set; } = default!;

        public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();
        


        public static Cafe Create(CafeId id, string name, string description,
            string location, string logo = "")
        {
            return new Cafe
            {
                Id = id,
                Name = name,
                Description = description,
                Logo = logo,
                Location = location
            };

        }

        public void Update(string name, string description, string location, string logo)
        {
            Name = name;
            Description = description;
            Location = location;
            if (!string.IsNullOrEmpty(logo)) {
                Logo = logo;
            }
        }

        public void AddEmployee(Employee employee)
        {
            AddEmployee(employee, null);
        }

        public void AddEmployee(Employee employee, DateTime? startDate)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));
            if (_employees.Any(e=>e.Id == employee.Id))
            {
                throw new DomainException($"{nameof(Employee)} already exists");
            }
            employee.StartWork(Id, startDate);
            _employees.Add(employee);
            
        }

        public void RemoveEmployee(EmployeeId employeeId)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

        
    }
}
