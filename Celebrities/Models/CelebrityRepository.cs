using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Celebrities.Models
{
    /* Celebrity Repository handles data accsess (json) */
    public class CelebrityRepository
    {
        internal CelebrityModel Create()
        {
            CelebrityModel celebrity = new CelebrityModel();
            return celebrity;
        }

        internal List<CelebrityModel> Retrieve()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/celebrities.json");
            var json = System.IO.File.ReadAllText(filePath);
            var celebrities = JsonConvert.DeserializeObject<List<CelebrityModel>>(json);
            return celebrities;
        }

        internal CelebrityModel Save(CelebrityModel celebrity)
        {
            var celebrities = this.Retrieve();
            int maxId;
            if(celebrities.Count() == 0)
            {
                maxId = 0;
            }
            else
            {
                maxId = celebrities.Max(c => c.Id);
            }
            celebrity.Id = maxId + 1;
            celebrities.Add(celebrity);

            WriteData(celebrities);
            return celebrity;
        }

        internal CelebrityModel Save(int id, CelebrityModel celebrity)
        {
            var celebrities = this.Retrieve();
            var orignialCelebrity = celebrities.Where(c => c.Id == id).FirstOrDefault();

            if(orignialCelebrity != null)
            {
                celebrities.Remove(orignialCelebrity);
                celebrities.Add(celebrity);
            }
            else
            {
                return null;
            }

            WriteData(celebrities);
            return celebrity;
        }

        private bool WriteData(List<CelebrityModel> celebrities)
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/celebrities.json");

            var json = JsonConvert.SerializeObject(celebrities, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }

        public void Delete(int id)
        {
            var celebrities = this.Retrieve();
            var celebrity = celebrities.Where(c => c.Id == id).FirstOrDefault();
            if(celebrity != null)
            {
                celebrities.Remove(celebrity);
                WriteData(celebrities);
            }
        }
    }
}