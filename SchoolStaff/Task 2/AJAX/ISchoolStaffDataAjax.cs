using System.Collections.Generic;

namespace Presentation.AJAX
{
    public interface ISchoolStaffDataAjax
    {
        IEnumerable<SchoolStaffSentJson> RetAjax(string name);
    }
}