using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }

        public Form1()
        {
            InitializeComponent();
            
            this.Da = new DataAccess();
            //string sql = "select * from phoneBook";
            this.PopulateGridView();
        }

        public void PopulateGridView(string sql = "select * from phoneBook")
        {
            
            this.dgvMain.AutoGenerateColumns = false;
            this.Ds = this.Da.ExecuteQuery(sql);
            this.dgvMain.DataSource = this.Ds.Tables[0];
        }

        public void refresh()
        {
            this.idBox.Text = "";
            this.idBox.Enabled = true;
            this.nameBox.Text = "";
            this.phnBox.Text = "";
            this.emailBox.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string sql = @"select * from phoneBook where name like '%" + searchBox.Text + "%' or phoneNo like '%" + searchBox.Text + "%';";
            this.PopulateGridView(sql);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {

            string sql = @"select * from phoneBook where id='" + idBox.Text + "';";
            this.Ds = this.Da.ExecuteQuery(sql);

            if (this.Ds.Tables[0].Rows.Count == 1)
            {
                sql = @"update phoneBook set id='" + idBox.Text + "',name='" + nameBox.Text + "',phoneNo='" + phnBox.Text + "',email='" + emailBox.Text + "' where id='" + idBox.Text + "';";
                this.Da.ExecuteUpdateQuery(sql);
                MessageBox.Show(this.idBox.Text + " has been Updated!!");
                this.refresh();
                this.PopulateGridView();
            }

            else
            {
                if (idBox.Text == "" || nameBox.Text == "" || phnBox.Text == "" || emailBox.Text == "")
                {
                    MessageBox.Show("Please Provide all the required data");
                }
                else
                {
                    sql = @"insert into phoneBook values('" + idBox.Text + "','" + nameBox.Text + "','" + phnBox.Text + "','" + emailBox.Text + "');";
                    this.Da.ExecuteUpdateQuery(sql);
                    MessageBox.Show("Data Inserted!!");
                    this.PopulateGridView();
                    this.refresh();
                }
                


            }
        }

        private void dgvMain_DoubleClick(object sender, EventArgs e)
        {
            this.idBox.Text = this.dgvMain.CurrentRow.Cells["id"].Value.ToString();
            this.idBox.Enabled = false;
            this.nameBox.Text = this.dgvMain.CurrentRow.Cells["name"].Value.ToString();
            this.phnBox.Text = this.dgvMain.CurrentRow.Cells["phoneNo"].Value.ToString();
            this.emailBox.Text = this.dgvMain.CurrentRow.Cells["email"].Value.ToString();
            this.idBox.Enabled = false;
        }


        private void dltBtn_Click(object sender, EventArgs e)
        {
            string id = this.dgvMain.CurrentRow.Cells["id"].Value.ToString();
            string sql = @"delete from phoneBook where id='" + id + "'";
            this.Ds = this.Da.ExecuteQuery(sql);
            this.PopulateGridView();
            this.refresh();
        }

        private void rstBtn_Click(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void dgvMain_Click(object sender, EventArgs e)
        {

        }

    }
}
