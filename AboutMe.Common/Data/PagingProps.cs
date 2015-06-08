using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Data
{
    public class PagingProps
    {
        
        /**
        public int page_size { get; set; } //한페이지
        public int cur_page { get; set; }
        public int total_cnt { get;set; }
        public string query_str { get;set; }
        public int max_page { get; set; }
        public int page_per_block { get; set; }
        **/

        public int CurrentBlock { get; set; }
        public int CurrentPage { get; set; }
        public int PagePerBlock { get; set; }
        public string QStr { get; set; }


        public int TotalNumOfPage { get; set; }
        public int TotalNumOfBlock { get; set; }
        public int StartPage { get; set; } //현재블럭에서 Numbering의 첫페이지 번호 
        public int EndPage { get; set; } //현재블럭에서 Numbering의 끝페이지 번호 


        /**
        private int totalNumOfBlock = 1; //블럭의 갯수 
        private int currentBlock = 1; //현재블럭
        private int startPage = 1;
        private int endPage = 1;
        **/
        //totalNumOfPage = max_page


        //                    전체레코드,    페이지당 레코드수(10) , 블럭당페이지수(10), 현재페이지 , 추가파라미터 
        public PagingProps(int TotalRecord, int RecordPerPage, int pPagePerBlock, int pCurrentPage, string qStr)
        {

            this.CurrentPage  = pCurrentPage ;
            this.PagePerBlock = pPagePerBlock ;
            this.QStr = qStr;
            
            this.TotalNumOfPage = (int)Math.Ceiling((double)TotalRecord / RecordPerPage); // 전체 페이지의 갯수
            this.TotalNumOfBlock = (int)Math.Ceiling((double)TotalNumOfPage / pPagePerBlock); // 전체 블럭 갯수
            this.CurrentBlock = (int)Math.Ceiling((double)CurrentPage / pPagePerBlock); // 현재 블럭

            this.StartPage = (CurrentBlock -1) * pPagePerBlock+1 ;
            this.EndPage = StartPage + pPagePerBlock - 1;
            if (EndPage > TotalNumOfPage)
                EndPage = TotalNumOfPage;

            if(TotalRecord == 0)
            {
                StartPage = 1;
                EndPage = 1;
                TotalNumOfPage = 1;
            }


           
         
            
            //this.total_cnt = t_cnt;
            //this.page_size = p_size;
            //this.cur_page = page;
            //this.query_str = q_str;
            //this.max_page = (int)Math.Ceiling((double)this.total_cnt / this.page_size); //올림

            //this.totalNumOfBlock = (int)Math.Ceiling((double)max_page / page_per_block); // 블럭의 갯수
            //this.currentBlock =  (int)Math.Ceiling((double)cur_page/page_per_block);
            //this.startPage = (currentBlock - 1) * page_per_block - 1;

        }


        public int GoPageOfPrevBlock //이전블럭의 첫페이지 번호
        {
            get 
            {
                if (this.HasPreviousBlock == true)
                    return (StartPage - PagePerBlock);
                else
                    return 0 ;
            }
        }
        public int GoPageOfNextBlock //다음블럭의 첫페이지 번호
        {
            get
            {
                if (this.HasNextBlock== true)
                    return (StartPage + PagePerBlock);
                else
                    return 0;
            }
        }



        public bool HasPreviousBlock
        {
            get { return (CurrentBlock > 1 && TotalNumOfBlock != 1 ); }
        }
        public bool HasNextBlock
        {
            get { return (CurrentBlock < TotalNumOfBlock && TotalNumOfBlock != 1); }
        }


        public bool HasPreviousPage
        {
            get { return (CurrentPage > 0); }
        }
        public bool HasNextPage
        {
            get { return (CurrentPage + 1 < TotalNumOfPage); }
        }

        /**
        public PagingProps(int p_size, int page, int t_cnt,int  page_per_block, string q_str)
        {
            this.total_cnt = t_cnt;
            this.page_size = p_size ;
            this.cur_page = page;
            this.query_str = q_str;
            this.max_page = (int)Math.Ceiling((double)this.total_cnt / this.page_size); //올림
            
            //this.totalNumOfBlock = (int)Math.Ceiling((double)max_page / page_per_block); // 블럭의 갯수
            //this.currentBlock =  (int)Math.Ceiling((double)cur_page/page_per_block);
            //this.startPage = (currentBlock - 1) * page_per_block - 1;
           
        }
        **/
  


    }
}
