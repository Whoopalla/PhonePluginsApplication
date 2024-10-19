using PhoneApp.Domain;
using PhoneApp.Domain.DTO;

namespace EmployeesFatcher {
    public class Container {
        public List<User> users { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class User {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string maidenName { get; set; }
        public string phone { get; set; }
    }
}