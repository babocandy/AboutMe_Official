using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminDisplay;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;

namespace AboutMe.Domain.Service.AdminDisplay
{
    public class AdminDisplayService : IAdminDisplayService
    {
        /**
         * 이미지 타입 전시물 저장
         */
        public void SaveImageTypeDisplayer(int? idx, string kind, string subKind = null, string url = null, string img = null, int? seq = null)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                //context.SP_ADMIN_DISPLAY_SAVE(idx, kind, subKind, seq, url, img, null, null, null, null, null);
                context.SP_ADMIN_DISPLAY_SAVE(idx, kind, subKind, seq, url, img, null, null, null, retNum, retMsg);
            }
        }

        /**
         * 상품 타입 전시물 저장
         */
        public Tuple<string, string> SaveProductTypeDisplayer(int? idx, string kind, string subKind = null, string pcode = null, int? seq = null)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_SAVE(idx, kind, subKind, seq, null, null, pcode, null, null, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            return tp;
        }

        /**
         * 링크 타입 전시물 저장
         */
        public Tuple<string, string> SaveLinkTypeDisplayer(int? idx, string kind, string subKind = null, int? seq = null, string title1 = null, string title2 = null, string url = null)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_SAVE(idx, kind, subKind, seq, url, null, null, title1, title2, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            return tp;
        }

        /**
         * 전시물 삭제
         */
        public void RemoveDisplayer(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_REMOVE(idx);
            }
        }

        /**
         * 전시물의 이미지만 삭제
         */
        public void RemoveOnlyImageInDisplayer(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_IMAGE_REMOVE(idx);
            }
        }

        /**
         * 전시물 조회
         */
        public List<SP_ADMIN_DISPLAY_SEL_Result> GetDisplayerList(string kind, string subKind = null, int? seq = null)
        {
            List<SP_ADMIN_DISPLAY_SEL_Result> list = new List<SP_ADMIN_DISPLAY_SEL_Result>();
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                list = context.SP_ADMIN_DISPLAY_SEL( kind, subKind, seq).ToList();
            }

            return list;
        }

    }
}
