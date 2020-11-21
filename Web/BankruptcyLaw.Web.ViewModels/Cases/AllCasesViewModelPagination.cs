using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptcyLaw.Web.ViewModels.Cases
{
    public class AllCasesViewModelPagination
    {
        public IEnumerable<SingleCaseViewModel> Cases { get; set; }

        public int Id { get; set; } // this is the page but we name it ID because we'll be using the controller/action/Id routing
    }
}
