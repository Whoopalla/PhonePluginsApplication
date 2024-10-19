using PhoneApp.Domain.DTO;

namespace EmployeesFatcher {
    public interface IFethcer {
        public Task<IEnumerable<EmployeesDTO>> GetEmployees();
    }
}