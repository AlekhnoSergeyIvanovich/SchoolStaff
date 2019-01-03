using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.AJAX
{
    public interface IProfessionDataAjax
    {
        IEnumerable<ProfessionSentJson> RetAjax(string name);
    }
}
