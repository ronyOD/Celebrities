using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Celebrities.Models;
using System.Web.Http.Cors;

namespace Celebrities.Controllers
{
    [EnableCorsAttribute("http://localhost:50728", "*","*")]

    /* Celebrities Controller handles CRUD operations */
    public class CelebritiesController : ApiController
    {
        public void Delete(int id)
        {
            var celebrityRepository = new CelebrityRepository();
            celebrityRepository.Delete(id);
        }

        public void Put(int id, [FromBody]CelebrityModel celebrity)
        {
            var celebrityRepository = new CelebrityRepository();
            var updatedProduct = celebrityRepository.Save(id, celebrity);
        }

        public CelebrityModel Get(int id)
        {
            CelebrityModel celebrity;
            var celebrityRepository = new CelebrityRepository();

            if(id > 0)
            {
                var celebrities = celebrityRepository.Retrieve();
                celebrity = celebrities.FirstOrDefault(p => p.Id == id);
            }
            else
            {
                celebrity = celebrityRepository.Create();
            }

            return celebrity;
        }

        
        public List<CelebrityModel> Get()
        {
            var celebrityRepository = new CelebrityRepository();
            return celebrityRepository.Retrieve();

        }

        public void Post([FromBody] CelebrityModel celebrity)
        {
            var celebrityRepository = new CelebrityRepository();
            var newCelebrity = celebrityRepository.Save(celebrity);

        }
    }
}
