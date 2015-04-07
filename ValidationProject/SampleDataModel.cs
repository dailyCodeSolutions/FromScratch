using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationProject.ValidationRules;

namespace ValidationProject
{
    public class SampleDataModel : ValidationProject.Base.DataModelBase
    {
        public string Email { get; set; }

        protected override void AddValidationRules()
        {
            base.AddValidationRules();

            AddValidationRule(() => Email, new RequiredRule()
            {
                PropertyRequired = () => Email,
                ErrorMessage = "Email is required"
            });
        }

    }
}
