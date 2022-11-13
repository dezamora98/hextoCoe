using System;
using System.IO;

namespace hextocoe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextReader hexDoc = new StreamReader(args[0]);
            TextWriter coeDoc = new StreamWriter(args[1]);

           // UInt16 addrSize = Convert.ToUInt16(args[2],16);

            coeDoc.WriteLine("memory_initialization_radix=16;");
            coeDoc.WriteLine("memory_initialization_vector=");

            string line = hexDoc.ReadLine();
            UInt16 addr = 0x0000;

            while(line != null)
            {

                if(line.StartsWith(":"))
                {
                    byte lineType = Convert.ToByte(line.Substring(7, 2), 16);
                    switch(lineType)
                    {
                        case 0x00:   
                            byte lineSize = Convert.ToByte(line.Substring(1, 2), 16);
                            UInt16 lineAddr = Convert.ToUInt16(line.Substring(3, 4), 16);

                            if (addr != 0)
                                coeDoc.WriteLine(',');
                            
                            while(addr != lineAddr)
                            {
                                coeDoc.WriteLine("00,");
                                ++addr;
                            }

                            for(byte i = 0; i < lineSize; ++i)
                            {
                                if (i+1 != lineSize)
                                    coeDoc.WriteLine(line.Substring((i*2) + 9, 2) + ',');
                                else
                                    coeDoc.Write(line.Substring((i * 2) + 9, 2));
                                ++addr;
                            }
                            break;

                        case 0x01:
                            /*  if (addr < addrSize)
                                    {
                                         while (addr++ != addrSize)
                                         {
                                             if(addr != addrSize)
                                                 coeDoc.WriteLine("FF,");
                                             else
                                                 coeDoc.WriteLine("FF;");
                                         }
                                     }
                                     else
                                     {
                                         coeDoc.Write(';');
                                     }
                            */
                            coeDoc.Write(';');
                            break;
                            
                    }
                }
                line = hexDoc.ReadLine();
            }

            hexDoc.Close();
            coeDoc.Close();
            return;
        }
    }
}
