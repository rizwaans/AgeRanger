using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace AR
{
    public partial class Details : System.Web.UI.Page
    {
        // declaring variables for usage across the context
        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataAdapter DB;
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        string CommandText;

        protected void Page_Load(object sender, EventArgs e)
        {
            setConnection();
        }
        //connection string, change the .db file url in webconfig file, point to your local system path where file is resided.
        private void setConnection()
        {
           con = new SQLiteConnection(ConfigurationManager.ConnectionStrings["datasource"].ConnectionString);
           // con = new SQLiteConnection("Data Source= C:\\Users\\rizwan\\Downloads\\AgeRanger.db ; New= False");
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "insert into person (FirstName,LastName,Age) values('" + txt_fName.Text.Trim() + "','" + txt_lname.Text.Trim() + "','" + txt_age.Text.Trim() + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Response.Write("<script>alert(ex.message)</script>"); }
            finally
            {
                cleardata();
                con.Close();
                LoadData("save");
            }
        }

        private void LoadData( string action)
        {
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                if (action != "search")
                    CommandText = "select person.id, person.firstname, person.lastname, person.age, agegroup.description from person INNER JOIN Agegroup where person.age >= agegroup.MinAge AND person.age < agegroup.MaxAge";
                else
                {
                    if (txt_fName.Text != string.Empty && txt_lname.Text != string.Empty)
                        CommandText = "select person.id, person.firstname, person.lastname, person.age, agegroup.description from person INNER JOIN Agegroup where person.firstname = " + txt_fName.Text + " OR person.lastname= " + txt_lname.Text + " AND person.age >= agegroup.MinAge AND person.age < agegroup.MaxAge";
                    else if (txt_fName.Text != string.Empty)
                        CommandText = "select a.*, b.description from person a INNER JOIN agegroup b where a.firstname= '" + txt_fName.Text.Trim() + "' AND  a.age  >= b.MinAge AND  a.age <  b.MaxAge";
                    else
                        CommandText = "select a.*, b.description from person a INNER JOIN agegroup b where a.lastname= '" + txt_lname.Text.Trim() + "' AND  a.age  >= b.MinAge AND  a.age <  b.MaxAge";
                }
                DB = new SQLiteDataAdapter(CommandText, con);
                DS.Reset();
                DB.Fill(DS);
                if (DS.Tables[0].Rows.Count != 0)
                {
                    DT = DS.Tables[0];
                    Grid.DataSource = DT;
                    Grid.DataBind();
                }
                else
                { Response.Write("<script>alert('No Data Found')</script>"); }
            }
            catch (Exception ex)
            { Response.Write("<script>alert(ex.message)</script>"); }
            finally
            {
                con.Close();
            }
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            cleardata();
        
           
        }
        private void cleardata()
        {
            txt_age.Text = "";
            txt_fName.Text = "";
            txt_lname.Text = "";
           // lbl_txt.Text = "";
        }

        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GridViewRow row = (GridViewRow)Grid.Rows[e.RowIndex];
                var id = row.Cells[1].Text.ToString();
                con.Open();
                cmd = new SQLiteCommand("delete from person where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                LoadData("save");
            }
        }

        protected void Grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Grid.Rows[e.RowIndex];
                con.Open();
                cmd = new SQLiteCommand("update person set firstname = '" + ((TextBox)(row.Cells[2].Controls[0])).Text + "', lastname = '" + ((TextBox)(row.Cells[3].Controls[0])).Text + "',age = '" + ((TextBox)(row.Cells[4].Controls[0])).Text + "'", con);
                cmd.ExecuteNonQuery();
              
                Grid.EditIndex = -1;
                LoadData("update");
            }
            catch(Exception ex)
            { Response.Write("<script>alert(ex.message)</script>"); }
            finally
            { con.Close(); }
        }

        protected void Grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var id = Grid.Rows[e.NewEditIndex].Cells[1].Text.ToString();
            LoadData("save");
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            //lbl_txt.Text = "";
            if (txt_age.Text != string.Empty)
                Response.Write("<script>alert('Please search using first name or lastname')</script>"); 
            else
            {
                //txt_age.Enabled = false;

                LoadData("search");
                cleardata();
            }
        }
    }
}