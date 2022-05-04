using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Helpes
{
    public class Library
    {
        public byte[] CopyPhoto(byte[] obj)
        {
            byte[] result = new byte[obj.Length];
            for (int i = 0; i < obj.Length; i++)
                result[i] = obj[i];
            return result;
        }

        public string PhoneNumberValidation(string ph)
        {

            if (ph == null ||  ph.Length < 13) return null;
            int len = ph.Length;
            string tmp = null,
                   plus = null;            

                if ((int)ph[0] == 43)//if first symbol is "+"
                    tmp += ph[0];
                else
                    return null;

                for (int i = 1; i < ph.Length; i++)
                    if ((int)ph[i] >= 48 && (int)ph[i] <= 57)
                        tmp += ph[i];
                    else
                    {
                        if (ph[i] == '+')
                            return null;
                    }

            return tmp;

        }

    }
}
