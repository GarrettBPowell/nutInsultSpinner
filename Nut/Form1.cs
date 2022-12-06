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


namespace Nut
{
    public partial class Form1 : Form
    { 
        IDictionary<int, string> nutNames = new Dictionary<int, string>();
        IDictionary<int, string> adjs = new Dictionary<int, string>();
        int numOfNuts = 0;
        int numOfAdjs = 0;

        public Form1()
        {
            InitializeComponent();

        // load csvs in queryable dictionary
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\Resources\nuts.csv");
            string sFilePath = Path.GetFullPath(sFile);

            using (var reader = new StreamReader(sFilePath)){
                while (!reader.EndOfStream) {
                    nutNames.Add(numOfNuts, reader.ReadLine());
                    numOfNuts++;
                }
            }

            sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\Resources\adjs.csv");
            sFilePath = Path.GetFullPath(sFile);

            using (var reader = new StreamReader(sFilePath))
            {
                while (!reader.EndOfStream)
                {
                    adjs.Add(numOfAdjs, reader.ReadLine());
                    numOfAdjs++;
                }
            }
        }

        public void writeLine(String fileName, String line) {
            using (StreamWriter writer = new StreamWriter(fileName, append: true)) {
                writer.WriteLine(line);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // pick nut
            String nutWord = "";
            while (nutWord.Equals(""))
            {
                Random rnd = new Random();
                int nutNum = rnd.Next(0, nutNames.Count());

                nutNames.TryGetValue(nutNum, out nutWord);
            }

            // pick adj
            String adjWord = "";
            while (adjWord.Equals(""))
            {
                Random rnd = new Random();
                int adjNum = rnd.Next(0, adjs.Count());

                adjs.TryGetValue(adjNum, out adjWord);
            }

            // set text
            textBox1.Text = adjWord;
            textBox2.Text = nutWord;
        }

        //save ADJ
        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(""))
            {
                String input = textBox3.Text;
                String check = "";
                adjs.TryGetValue(numOfAdjs - 1, out check);

                if (check != null)
                {
                    adjs.Add(numOfAdjs, input);

                    string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\Resources\adjs.csv");
                    string sFilePath = Path.GetFullPath(sFile);

                    numOfAdjs++;
                    writeLine(sFilePath, input);
                }
            }

            textBox3.Text = "Add an Adj";
        }

        //save NUT
        private void button3_Click(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(""))
            {
                String input = textBox4.Text;
                String check = "";
                nutNames.TryGetValue(numOfNuts - 1, out check);

                if (check != null)
                {
                    adjs.Add(numOfNuts, input);

                    string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\Resources\adjs.csv");
                    string sFilePath = Path.GetFullPath(sFile);

                    numOfNuts++;

                    writeLine(sFilePath, input);
                }
            }

            textBox4.Text = "Add a Nut";
        }

        private void textBox3_Click(object sender, System.EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox4_Click(object sender, System.EventArgs e)
        {
            textBox4.SelectAll();
        }
    }
}
