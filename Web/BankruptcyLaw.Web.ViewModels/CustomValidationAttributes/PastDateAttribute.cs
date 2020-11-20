namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue <= DateTime.UtcNow.Date)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
