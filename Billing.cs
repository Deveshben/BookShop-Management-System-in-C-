﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookShop
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\admin\OneDrive\Documents\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            BTtitleTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            ClientNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (ClientNameTb.Text == "" || BTtitleTb.Text == "")
            {
                MessageBox.Show("Select Client Name");
            }
            else
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                try
                {
                    Con.Open();
                    string query = "insert into UserTbl values('" + UserNameLbl.Text + "','" + ClientNameTb.Text + ",'" + Grdtotal + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill saved successfully");
                    Con.Close();
                   
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);

                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void CatCbSearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BCatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
           
        }
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            Con.Open();
            string query = "update BookTbl set BQty=" + newQty + "where BId=" + key + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Book updated successfully");
            Con.Close();
            populate();
            //Reset(); 
        }
        int n = 0,Grdtotal=0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("Not enough stock");

            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTtitleTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PriceTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                Grdtotal = Grdtotal + total;
                Totallbl.Text = "Rs" + Grdtotal;

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0,stock=0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTtitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            //QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString(); 
            if (BTtitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BTtitleTb.Text = BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BTtitleTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ClientNameTb_TextChanged(object sender, EventArgs e)
        {

        }
        int prodid, prodqty, prodprice, tottal, pos = 60;
        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Book shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("Id Product Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand total:RS" + Grdtotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("***Book store***", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            Grdtotal = 0;

        }
        
        private void printPreviewDialog1_Load(object sender,EventArgs e)
        {
            
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = login.Username;
        }

        private void label25_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }
    }
}
