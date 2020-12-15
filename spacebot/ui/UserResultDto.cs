using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacebot.ui
{
    public class UserResultDto : IComparable
    {
        public string name;
        public string datetime;
        public double result;

        public int CompareTo(object obj)
        {
            if(obj is UserResultDto)
            {
                UserResultDto temp = obj as UserResultDto;
                return (int)(temp.result - result);
            }
            return -1;
        }
    }
}
