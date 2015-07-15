using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Helper
{
    public class MemberCode2Label
    {
        public static string Grade2Label(string code)
        {
            var label = "";
            switch (code)
            {
                case "B": label = "브론즈";
                    break;
                case "S": label = "실버";
                    break;
                case "G": label = "골드";
                    break;
                case "V": label = "VIP";
                    break;

            }

            return label;

        }

        public static string Gbn2Label(string code)
        {

            var label = "";
            switch (code)
            {
                case "A": label = "일반회원";
                    break;
                case "S": label = "임직원";
                    break;
            }

            return label;
        }
    }
}
