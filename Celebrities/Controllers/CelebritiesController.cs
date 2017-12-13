using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Celebrities.Models;
using Newtonsoft.Json;

namespace Celebrities.Controllers
{
    public class CelebritiesController : BaseController
    {
        
        public bool Delete(int Id)
        {
            var CelebrityToDelete = GetById(Id);
            if (CelebrityToDelete != null)
            {
                this.celebsList.Remove(CelebrityToDelete);
                SaveAll();
                return true;
            }
            return false;
        }

        public bool Put(int Id,[FromBody]CelebrityModel UpdatedCelebrity)
        {
            var Celebrity = GetById(Id);
            if(Celebrity != null)
            {
                Delete(Id);
                UpdatedCelebrity.Id = Id;
                celebsList.Add(UpdatedCelebrity);
                SaveAll();
                return true;
            }
            return false;
        }

        public List<CelebrityModel> Get()
        {
            return this.celebsList;
        }

        public bool Post([FromBody] CelebrityModel Celebrity)
        {
            Celebrity.Id = celebsList.Count + 1;
            if(Celebrity.Name != null && Celebrity.Age != 0 && Celebrity.Country != null)
            {
                celebsList.Add(Celebrity);
                SaveAll();
                return true;
            }
            return false;
        }

        private bool SaveAll()
        {
            if (WriteToJsonFile())
            {
                return true;
            }
            return false;
        }

        private bool WriteToJsonFile()
        {
            //serialize list to json and write to file
            try
            {
                var JsonStr = JsonConvert.SerializeObject(this.celebsList.ToArray());
                System.IO.File.WriteAllText(this.Path, JsonStr);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        private CelebrityModel GetById(int Id)
        {
            try
            {
                var Celebrity = celebsList.Find(x => x.Id == Id);
                if(Celebrity != null)
                {
                    return Celebrity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
    }
}
