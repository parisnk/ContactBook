using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactBookApp
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public Form1()
        {
            InitializeComponent();
        }

        // save button

        private void button2_Click(object sender, EventArgs e)
        {
            Phone[] newNumbers = new Phone[dataGridView1.RowCount - 1];
            for (int i = 0; i < newNumbers.Length; i++)
            {
                newNumbers[i] = new Phone();
                newNumbers[i].ContactName = dataGridView1[3, i].Value.ToString();
                newNumbers[i].ContactNumber = dataGridView1[4, i].Value.ToString();
                db.Phones.InsertOnSubmit(newNumbers[i]);
            }
            db.SubmitChanges();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        //view mode
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db = new DataClasses1DataContext();
            //Connect to the data source
            dataGridView1.DataSource = db.Phones;
            dataGridView1.Columns["dbName"].Visible = true;
            dataGridView1.Columns["dbNumber"].Visible = true;
            dataGridView1.Columns["Name1"].Visible = false;
            dataGridView1.Columns["Number"].Visible = false;
            button2.Enabled = false;
            dataGridView1.Refresh();
        }

        //insert mode
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //disconnect from datasource
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns["dbName"].Visible = false;
            dataGridView1.Columns["dbNumber"].Visible = false;
            dataGridView1.Columns["Name1"].Visible = true;
            dataGridView1.Columns["Number"].Visible = true;
            button2.Enabled = true;
            dataGridView1.Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);

        }

        //search button
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;

            var query =
                from phone in db.Phones
                where phone.ContactName.Contains(search) || phone.ContactNumber.Contains(search)
                select phone;

            dataGridView1.DataSource = query;
            dataGridView1.Refresh();
        }
    }
}
