using PhoneApp.Domain.DTO;
using Newtonsoft.Json;
using NLog.Targets;
using NLog;

namespace EmployeesFatcher {
    internal class Fethcer : IFethcer {
        private NLog.Logger _logger;
        internal Fethcer(NLog.Logger logger) {
            _logger = logger;
        }

        Uri employeesUri = new Uri("https://dummyjson.com/users");

        public async Task<IEnumerable<EmployeesDTO>> GetEmployees() {
            _logger.Info("Loading with fetcher");
            List<EmployeesDTO> result = new List<EmployeesDTO>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(employeesUri);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                _logger.Error($"Http response code: {response.StatusCode}");
                return result;
            }
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
            var body = await response.Content.ReadAsStringAsync();
            Container container = JsonConvert.DeserializeObject<Container>(body);
            _logger.Info($"Fetched: {container.users.Count} employees");

            return UsersToEmployees(container.users);
        }

        public static List<EmployeesDTO> UsersToEmployees(IEnumerable<User> users) {
            List<EmployeesDTO> res = new List<EmployeesDTO>(users.Count());
            EmployeesDTO newEmployee;
            foreach (var user in users) {
                newEmployee = new EmployeesDTO() {
                    Name = $"{user.lastName} {user.firstName} {user.maidenName}",
                };
                newEmployee.AddPhone(user.phone);
                res.Add(newEmployee);
            }
            return res;
        }
    }
}