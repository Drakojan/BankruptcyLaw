namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IJudgesService
    {
        public IEnumerable<KeyValuePair<int, string>> GetJudgesNamesAndIds();
    }
}
