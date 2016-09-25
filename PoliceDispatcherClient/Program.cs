using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PoliceDispatcherClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Define log file location 
            string path = Application.StartupPath + @"\Errorlog.txt";

            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            // Catch all exceptions not handled 
            catch (Exception excep)
            {
                // Write the exception's data to a log file 
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine(DateTime.Now + ": Source: " + excep.Source + " Message: " + excep.Message);
                }

                // Offer a choice of whether or not to start a new instance of the program.
                DialogResult res = MessageBox.Show("An error has occured that is undefined, please check the logs in your current folder for more details! \n"
                    + " Please click yes to Continue and try again or Exit the application?", "Try Again?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }
    }
}
