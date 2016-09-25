using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecureCommunicationComponent;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization;

namespace PoliceDispatcherClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Communicator com = new Communicator();
            MessageBox.Show(com.SendAlert(textBox3.Text, "You've been called for an incident"));

            AddreseeInput addin = new AddreseeInput();

            try
            {
                addin.showAddressee(textBox2.Text);
            }

            catch (SecureCommunicationException ex)
            {
                // Pop up a message box 
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(@"C:\Users\fkhalil\Documents\Visual Studio 2015\Projects\E33FKHALILHW0\SecureCommunicationComponent\codes.txt");
                string line = sr.ReadLine();

                while (line != null)
                {
                    comboBox1.Items.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
// Create a class to handle the cases for client input when the client calls SendAlert
public class AddreseeInput
{
    private CultureInfo ci = null;
    public void showAddressee(string inptadd)
    {
        // If name starts with 'A' or 'a', then create a  new SecureCommunicationException and throw it
        if (inptadd.StartsWith("A", true, ci))
        {
            SecureCommunicationException MyException = new SecureCommunicationException(
                String.Format("Record \"{0}\" is not Allowed.",
                inptadd));
            throw MyException;
        }

        // If name starts with 'B' or 'b', then perform a division by zero 
        // and allow the resulting exception to propagate upwards to the client. 
        else if (inptadd.StartsWith("B", true, ci))
        {
            DivideByZero();
        }

        // If name starts with 'C' or 'c', then set up a try block and perform the division by zero. 
        else if (inptadd.StartsWith("C", true, ci))
        {
            try
            {
                int zero = 0;
                int myInt = 1 / zero;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException(
                String.Format("You have entered a name that begins with 'C', /n" +
                "This forces a division by 0 Exception."));
            }
        }

        // Allow all other calls to proceed without exceptions
        else
        {
            throw new Exception(
                String.Format("An error has occured that is undefined, please check the logs!"));
        }
    }

    // Force a Division By Zero class
    public void DivideByZero()
    {
        int zero = 0;
        try
        {
            int myInt = 1 / zero;
        }

        catch (DivideByZeroException d)
        {
            throw new SecureCommunicationException("You have entered a name that begins with 'B' \n" +
                "and forced a division by 0.", d);
        }
    }
}

//Create a User-Defined exception class derived from Exception. 
public class SecureCommunicationException : System.Exception
{
    private string inptadd;

    //read-write string property 
    public string Addressee
    {
        get { return inptadd; }
        set { inptadd = value; }
    }

    const string message =
        "The value entered is not correct, please try again.";

    // Define three constructors.
    public SecureCommunicationException()
    { }

    public SecureCommunicationException(string message) : base(message)
    { }

    protected SecureCommunicationException(SerializationInfo info,
       StreamingContext context) : base(info, context)
    { }

    //fourth constructor - innerException
    public SecureCommunicationException(string message, Exception innerException) :
       base(message, innerException)
    { }
}


