using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.AJAX
{
    public class ProfessionSentJson
    {
        public int Id { get; set; }

        public string NameProfession { get; set; }

        public uint? CountPeopleProfession { get; set; }

        public bool IsExist { get; set; }
    }
}
