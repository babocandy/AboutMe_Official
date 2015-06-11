using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminProduct;

namespace AboutMe.Domain.Service.AdminProduct
{
    public interface IAdminProductService
    {
        List<SP_ADMIN_CATEGORY_ONE_SEL_Result> GetAdminCategoryOneList();
        
        int InsertAdminCategoryOne(string DEPTH1_NAME);
        //void InsertAdminCategoryOne(string DEPTH1_NAME);
        //List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList();
    }
}
