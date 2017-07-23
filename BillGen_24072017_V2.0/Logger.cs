using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BillGen
{
    public class Logger
    {
        static string fileName;
        public static string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        static string location;
        public static string Location
        {
            get { return location; }
            set
            {
                location = value;
                try
                {
                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);
                }
                catch { }
            }
        }

        static string ext = ".bgLog";

        static void CheckSize()
        {
            try
            {
                FileInfo fi = new FileInfo(Path.Combine(location, fileName + ext));
                if (fi.Exists)
                {
                    if (fi.Length > 10465290) //greater than 10 MB approx
                    {
                        string[] name = fileName.Split(new char[] { '_' });

                        if (name.Length == 2)
                        {
                            int num;
                            if (Int32.TryParse(name[1], out num))
                            {
                                num++;
                                fileName = name[0] + num;
                            }
                            else
                            {
                                num = 0;
                                do
                                {
                                    num++;
                                    fileName = name[0] + num;
                                }
                                while (File.Exists(Path.Combine(location, fileName + ext)));
                            }
                        }
                    }
                }
                else
                    fi.Create();
            }
            catch
            { }
        }

        /// <summary>
        /// Write Source,method,date,time,computer,error and stack trace information to the text file
        /// </summary>        
        /// <param name="ex">Exception object to create log about</param>
        /// <returns>false if the problem persists</returns>
        public static bool WriteErrorLog(Exception ex)
        {
            bool bReturn = false;
            string strException = string.Empty;
            try
            {
                CheckSize(); //split files if size gets larger than 10 mb
                StreamWriter sw = new StreamWriter(Path.Combine(location, fileName + ext), true);

                sw.WriteLine("Source	   : " + ex.Source.ToString().Trim());
                sw.WriteLine("Method	   : " + ex.TargetSite.Name.ToString());
                sw.WriteLine("DateTime	   : " + DateTime.Now.ToString());
                sw.WriteLine("Computer	   : " + Dns.GetHostName().ToString());
                sw.WriteLine("Error		   : " + ex.Message.ToString().Trim());
                if (ex is System.Net.Sockets.SocketException)
                {
                    System.Net.Sockets.SocketException se = (System.Net.Sockets.SocketException)ex;
                    sw.WriteLine("Error Code   : " + se.ErrorCode);
                }
                sw.WriteLine("Stack Trace  : " + ex.StackTrace.ToString().Trim());
                sw.WriteLine("**-------------------------------------------------------------------**");
                sw.Flush();
                sw.Close();
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            return bReturn;
        }

    }
}
