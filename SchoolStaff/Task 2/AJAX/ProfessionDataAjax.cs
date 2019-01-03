using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Business.Repositories;
using DAL.Entities;

namespace Presentation.AJAX
{
    public class ProfessionDataAjax : IProfessionDataAjax
    {
        readonly IRepository<Profession> _repos;

        public ProfessionDataAjax(IRepository<Profession> repos)
        {
            _repos = repos;
        }

        public IEnumerable<ProfessionSentJson> RetAjax(string name)
        {
            var deserializedUser = "";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(name));
            var ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as string;
            ms.Close();
            name = deserializedUser;
            IEnumerable<ProfessionSentJson> selectedProfession;

            if (name == "")
            {
                selectedProfession = (IEnumerable<ProfessionSentJson>)(from t in _repos.GetAll()
                                                               orderby t.Id
                                                               select new ProfessionSentJson
                                                               {
                                                                   Id = t.Id,
                                                                   CountPeopleProfession = t.CountPeopleProfession,
                                                                   NameProfession = t.NameProfession,
                                                                   IsExist = true
                                                               });
            }
            else
            {
                selectedProfession = (IEnumerable<ProfessionSentJson>)(from t in _repos.GetAll()
                                                               orderby t.Id
                                                               select RetAjax(t, name));
            }
            return selectedProfession;
        }

        ProfessionSentJson RetAjax(Profession profession, string pName)
        {
            var bisExist = (profession.NameProfession.ToUpper().Contains(pName.ToUpper()));

            return new ProfessionSentJson
            {
                Id = profession.Id,
                NameProfession = bisExist ? profession.NameProfession : "",
                CountPeopleProfession = Convert.ToUInt32(bisExist ? profession.CountPeopleProfession.Value.ToString() : "0"),
                IsExist = bisExist
            };
        }
    }
}