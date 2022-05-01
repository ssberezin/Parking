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
            for (int i=0;i<obj.Length;i++)            
                result[i]=obj[i];
            return result;
        }

    }
}
