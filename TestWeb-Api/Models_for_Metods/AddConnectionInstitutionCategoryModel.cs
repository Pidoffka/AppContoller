using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models_for_Metods
{
    public class AddConnectionInstitutionCategoryModel
    {
        public int Id_Institution { get; set; }
        public List<SingleCategoryModel> Id_Categories { get; set; }
    }
}
