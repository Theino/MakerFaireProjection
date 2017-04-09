using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MakerFaireProjection
{
    public struct SensorData
    {
        public DateTime dateTime;
        public float value;
    }
    public partial class Form1 : Form
    {
        StreamReader reader;
        string currentLine;
        DateTime currFakeTime;
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
            SensorData firstData = PopulateSensorData(reader.ReadLine());
            currFakeTime = firstData.dateTime;
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

        private SensorData PopulateSensorData(string line)
        {
            string[] dataSplitString = line.Split(',');
            string[] dateSplit = dataSplitString[0].Split('/');
            string[] timeSplit = dataSplitString[1].Split(':');
            DateTime currDateTime = new DateTime(int.Parse(dateSplit[2]), int.Parse(dateSplit[1]), int.Parse(dateSplit[0]), int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), int.Parse(timeSplit[2]));
            SensorData currSensorData;
            currSensorData.dateTime = currDateTime;
            currSensorData.value = float.Parse(dataSplitString[2]);
            return currSensorData;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentLine = ReadCSVLine(currentLine);
            if (currentLine != null)
            {
                SensorData sensorData = PopulateSensorData(currentLine);

            }
        }
    }
}
