﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Domain.Service.AdminEtc
{
    public interface IAdminEtcErrorLogService
    {
        //어플리케이션에서 에러 발생시 에러로그 insert 
        void InsertAppErrlog(TB_APP_ERROR_LOG tb);
    }
}
