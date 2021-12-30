using http.Models;
using HttpClientService;
using MaxiService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace MaxiService
{
    public partial class ServiceClientApi
    {
        private BaseClientService baseClientService;

        public ServiceClientApi()
        {
            baseClientService = new BaseClientService(
                  "http://localhost:27968",
                  "secret",
                  "endpoint",
                  "maxitest",
                  "maxidirect",null,
                  "MaxiService");
        }

        public async Task<IEnumerable<Employee>> GetEmployees(Employee request)
        {
            var json = await baseClientService.GetAsyncWithErrors(request.ToQueryString(), "/Employees");

            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(json) ?? new List<Employee>();
        }
        public async Task<Employee> GetEmployeesById(Employee request)
        {
            var json = await baseClientService.GetAsyncWithErrors(request.ToQueryString(), "/Employees");

            return JsonConvert.DeserializeObject<Employee>(json) ?? new Employee();
        }

        public async Task<Response> CreateEmployees(Employee request)
        {
            var json = await baseClientService.PostAsyncWithErrors(request, "/Employees");

            return JsonConvert.DeserializeObject<Response>(json) ?? new Response();
        }
        public async Task<Response> UpdateEmployees(Employee request)
        {
            var json = await baseClientService.PutAsyncWithErrors(request, "/Employees");

            return JsonConvert.DeserializeObject<Response>(json) ?? new Response();
        }

        public async Task<IEnumerable<Beneficiary>> GetBeneficiaries(Beneficiary request)
        {
            var json = await baseClientService.GetAsyncWithErrors(request.ToQueryString(), "/Beneficiaries");

            return JsonConvert.DeserializeObject<IEnumerable<Beneficiary>>(json) ?? new List<Beneficiary>();
        }

        public async Task<Response> CreateBeneficiary(IEnumerable<Beneficiary> request)
        {
            var json = await baseClientService.PostAsyncWithErrors(request, "/Beneficiaries");

            return JsonConvert.DeserializeObject<Response>(json) ?? new Response();
        }
        public async Task<Response> UpdateBeneficiary(Beneficiary request)
        {
            var json = await baseClientService.PutAsyncWithErrors(request, "/Beneficiaries");

            return JsonConvert.DeserializeObject<Response>(json) ?? new Response();
        }
    }
}
