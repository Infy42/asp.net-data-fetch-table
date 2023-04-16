using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace csharp_assignment.Controllers
{
    public class RetrieveJSON
    {
        public Dictionary<string, double> EmployeeInfo = new Dictionary<string, double>();      //sadrzi sve radnike

        public Dictionary<string, double> first = new Dictionary<string, double>();

        public async Task RetrieveData()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("site.com{key}");          //key ide ovde

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    try
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        List<EmployeeInfo>? employees = JsonSerializer.Deserialize<List<EmployeeInfo>>(responseString);
                        
                        //grupisani i sortiranje radnika po satima
                        var employeeHours = employees.GroupBy(e => e.EmployeeName)
                                             .Select(g => new { EmployeeName = g.Key, TotalHours = g.Sum(e => (e.EndTimeUtc - e.StarTimeUtc).TotalHours) });


                        foreach (var employee in employeeHours)         //dodavanje radnika u 'first', sortiranje u 'EmployeeInfo'
                        {
                            TimeSpan timeSpan = TimeSpan.FromHours(employee.TotalHours);
                            string format = String.Format("{0}", (int)timeSpan.TotalHours);

                            first.Add(employee.EmployeeName, double.Parse(format));
                            EmployeeInfo = first.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                        }
                    }
                    catch (ArgumentNullException){ }        //catch zbog neimenovanog radnika
                }
            }
        }

        public Dictionary<string, double> Data()        //vracanje 'EmployeeInfo' kroz metodu
        {
            RetrieveData().GetAwaiter().GetResult();

            return EmployeeInfo;
        }
    }
}
