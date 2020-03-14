using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderList
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Initial();
            }
        }

        #region Initial

        private void Initial()
        {
            SetPlatForm("1");
            GridViewBind();
        }

        public void InitialDetail(DataTable dtDetail)
        {
            lblOrderId.Text = dtDetail.Rows[0]["Order_Id"].ToString();
            lblOrderItem.Text = dtDetail.Rows[0]["Order_Item"].ToString();
            lblPrice.Text = dtDetail.Rows[0]["Price"].ToString();
            lblCost.Text = dtDetail.Rows[0]["Cost"].ToString();
            lblStatus.Text = dtDetail.Rows[0]["Status"].ToString();
        }

        public void SetPlatForm(string Type)
        {
            switch (Type)
            {
                //顯示Oorder List
                case "1":
                    pnlList.Visible = true;
                    pnlDetail.Visible = false;
                    break;
                //顯示Detail
                case "2":
                    pnlList.Visible = false;
                    pnlDetail.Visible = true;
                    break;
            }
        }

        #endregion

        #region GridViewBind

        public void GridViewBind()
        {
            DataTable dtOrderList = new DataTable();
            dtOrderList = GetOrderList();

            gvList.DataSource = dtOrderList;
            gvList.DataBind();
        }

        #endregion

        #region Button

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string OrderId;
            int i;
            for (i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cb = gvList.Rows[i].Cells[0].FindControl("cb") as CheckBox;
                if (cb.Checked)
                {
                    OrderId = gvList.Rows[i].Cells[1].Text;
                    UpdateOrderList(OrderId);
                }
            }
            GridViewBind();
        }

        protected void lbtn_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = ((LinkButton)sender).NamingContainer as GridViewRow;
            int RowIndex = gvr.RowIndex;
            string Order_Id = gvList.Rows[RowIndex].Cells[1].Text;
            //string Order_Id= (((LinkButton)sender).NamingContainer.FindControl("lbtn") as LinkButton).Text;
            DataTable dtDetail = new DataTable();
            dtDetail = GetDetail(Order_Id);
            InitialDetail(dtDetail);
            SetPlatForm("2");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Initial();
            SetPlatForm("1");
        }

        #endregion

        #region SqlConnection & DataAccess

        public SqlConnection CreateConn()
        {
            string strConn = "Data Source=localhost;Initial Catalog=Customer;Persist Security Info=True;User ID=sa;Password=1";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            return sqlConnection;
        }

        public DataTable GetOrderList()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
select a.Order_Id, a.Order_Item, a.Price, a.Cost, b.Text 'Status'
from OrderList a
left join Parameter b on b.Value = a.Status
");
            using (SqlConnection sqlConnection = CreateConn())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = sb.ToString();
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = sqlCommand;
                    sqlDataAdapter.Fill(ds);
                }
            }

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public void UpdateOrderList(string OrderId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("update OrderList set Status='2' where Order_Id = @OrderID");

            using (SqlConnection sqlConnection = CreateConn())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = sb.ToString();
                    sqlCommand.Parameters.AddWithValue("@OrderID", OrderId);
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDetail(string Order_Id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
select a.Order_Id, a.Order_Item, a.Price, a.Cost, b.Text 'Status'
from OrderList a
left join Parameter b on b.Value = a.Status
where a.Order_Id = @Order_Id
");
            using (SqlConnection sqlConnection = CreateConn())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = sb.ToString();
                    sqlCommand.Parameters.AddWithValue("@Order_Id", Order_Id);
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = sqlCommand;
                    sqlDataAdapter.Fill(ds);
                }
            }

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        #endregion
    }
}