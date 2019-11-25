using ModelServices;
using Newtonsoft.Json;
using Repository.Implementations;
using System.Collections.Generic;
using System.Web.Http;


namespace CSharpAndMySQLApi.Controllers
{
    public class ValuesController : ApiController
    {
        private ModelService service = new ModelService(new ModelRepository((new WebConfigurationManager()).ConnectionString));
        // GET api/values
        public IEnumerable<string> Get()
        {
            var models = service.Get();
            List<string> ReturnList = new List<string>();
            foreach (var model in models)
            {
                ReturnList.Add(JsonConvert.SerializeObject(model));
            }
            return ReturnList;
        }

        // GET api/values/5
        public string Get(int id)
        {
            var model = service.Get(id);
            return JsonConvert.SerializeObject(model);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]Models.Model value)
        {
            return service.Post(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Models.Model value)
        {
            service.Put(id, value);
        }

        // DELETE api/values/5
        public bool Delete(int id)
        {
            return service.Delete(id);
        }
    }
}
