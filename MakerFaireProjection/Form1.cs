using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;'
using System.IO;

namespace MakerFaireProjection
{
    public struct SensorData
    {
        DateTime dateTime;
        float value;
    }
    public partial class Form1 : Form
    {
        StreamReader reader;
        string currentLine;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reader = new StreamReader("airmote.csv");
            if (reader == null)
                throw new Exception("Couldn't load csv");
            RemoveDescriptionLine();
            currentLine = ReadCSVLine(currentLine);
            string[] dataSplitString = currentLine.Split(',');
            string[] dateSplit = dataSplitString[0].Split('/');
            string[] timeSplit = dataSplitString[1].Split(':');
        }

        private string ReadCSVLine(string oldLine)
        {
            if (oldLine == "")
            {
                oldLine = reader.ReadLine();
                if (oldLine == null)
                {
                    throw new Exception("ERROR: Unable to read line in CSV");
                }
            }
            return oldLine;
        }

        private void RemoveDescriptionLine()
        {
            reader.ReadLine();
        }
    }
}
