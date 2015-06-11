using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;


namespace AboutMe.Domain.Service.AdminProduct
{
    public class AdminProductService : IAdminProductService
    {

        #region 1depth 카테고리 리스트
        public List<SP_ADMIN_CATEGORY_ONE_SEL_Result> GetAdminCategoryOneList()
        {

            List<SP_ADMIN_CATEGORY_ONE_SEL_Result> lst = new List<SP_ADMIN_CATEGORY_ONE_SEL_Result>();
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                /**try {**/
                lst = AdminProductContext.SP_ADMIN_CATEGORY_ONE_SEL().ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }
        #endregion

        #region 1depth 카테고리 등록 OUTPUT PARAMETER적용
        public int InsertAdminCategoryOne(string DEPTH1_NAME)
        {

            int i = 3 ;
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {

                ObjectParameter objParam = new ObjectParameter("INTRESULT", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                i = AdminProductContext.SP_ADMIN_CATEGORY_ONE_INS(DEPTH1_NAME, objParam);
                
            }
            return i;
        }
        #endregion
    }
}
