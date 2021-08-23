using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodStocks1
{
    public partial class Form1 : Form
    {
        StockFileRepository stockFileRepository = new StockFileRepository();
        //private object appData;


        public string columnNames { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "StockFile.csv";

                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data.");
                        }
                    }

                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string[] StockFileCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0;  i < columnCount ; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";    
                            }
                            StockFileCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    StockFileCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, StockFileCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully!", "Info");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                } 
            }
            else
            {
                MessageBox.Show("No Record To Export !");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = stockFileRepository.GetAll();
            //this.customersTableAdapter.Fill(this.appData.Customers);

        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "XML files|*.xml" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.WriteXml(sfd.FileName);
                        MessageBox.Show("You have successfully exported the file.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void WriteXml(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
