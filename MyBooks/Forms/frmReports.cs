using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    public partial class frmReports : Form
    {
        private bool bOnInit;
        private int iMode;

        public frmReports()
        {
            InitializeComponent();
            iMode = 0;
            bOnInit = false;
        }

        public void Init(DateTime dt)
        {
            bOnInit = true;
            dtFrom.Value = dt;
            dtTo.Value = dt;
            bOnInit = false;
        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            optMonth.Visible = User.Me.IsAdmin;
            optPeriod.Visible = User.Me.IsAdmin;
            optWeek.Visible = User.Me.IsAdmin;
            optYear.Visible = User.Me.IsAdmin;
            dtFrom.Enabled = User.Me.IsAdmin;
            dtTo.Enabled = User.Me.IsAdmin;
            CalculateReport();
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e) { CalculateReport(); }

        private void dtTo_ValueChanged(object sender, EventArgs e) { CalculateReport(); }

        private void CalculateReport()
        {
            if (bOnInit) return;
            DateTime dtF = dtFrom.Value.Date;
            DateTime dtT = dtTo.Value.Date;
            String sPeriod = "";
            String sFilter = "";
            String sDispName = "";
            switch (iMode)
            {
                case 0:
                    sPeriod = String.Format(Program.m_cif, "{0:dd} {1} {0:yyyy г.}", dtF, Program.m_cif.DateTimeFormat.MonthGenitiveNames[dtF.Month - 1]);
                    sFilter = String.Format(denSQL.Cif_sql, "bill_date LIKE '{0:yyyy-MM-dd}%'", dtF);
                    sDispName = String.Format(Program.m_cif, "{0:yyyyMMdd}_отчет", dtF);
                    break;
                case 1:
                    sPeriod = String.Format(Program.m_cif, " неделю c {0:dd.MM.yy} по {1:dd.MM.yy}", dtF.GetFirstDateOfWeek(), dtF.GetLastDateOfWeek());
                    sFilter = String.Format(denSQL.Cif_sql, "WEEK(bill_date)={0} AND YEAR(bill_date)={1}", dtF.GetWeekNumber(), dtF.Year);
                    sDispName = String.Format(Program.m_cif, "{0:yyyyMMdd}_нед_отчет", dtF);
                    break;
                case 2:
                    sPeriod = String.Format(Program.m_cif, " {1} {0:yyyy г.}", dtF, Program.m_cif.DateTimeFormat.MonthNames[dtF.Month - 1]);
                    sFilter = String.Format(denSQL.Cif_sql, "MONTH(bill_date)={0} AND YEAR(bill_date)={1}", dtF.Month, dtF.Year);
                    sDispName = String.Format(Program.m_cif, "{0:yyyyMM}_мес_отчет", dtF);
                    break;
                case 3:
                    sPeriod = String.Format(Program.m_cif, " {0:yyyy год}", dtF);
                    sFilter = String.Format(denSQL.Cif_sql, "YEAR(bill_date)={0}", dtF.Year);
                    sDispName = String.Format(Program.m_cif, "{0:yyyy}_отчет", dtF);
                    break;
                case 4:
                    sPeriod = String.Format(Program.m_cif, " период с {0:dd.MM.yy} по {1:dd.MM.yy}", dtF, dtT);
                    sFilter = String.Format(denSQL.Cif_sql, "bill_date >= '{0:yyyy-MM-dd}' AND bill_date < '{1:yyyy-MM-dd}'", dtF, dtT.AddDays(1).Date);
                    sDispName = String.Format(Program.m_cif, "{0:yyyyMMdd}-{1:yyyyMMdd}_отчет", dtF, dtT);
                    break;
            }
            this.Text = "Отчет за " + sPeriod;
            rptView.LocalReport.DisplayName = sDispName;
            List<Microsoft.Reporting.WinForms.ReportParameter> lrp = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parPeriod", sPeriod));
            rptView.LocalReport.SetParameters(lrp);
            dsMain.tblReport.Clear();
            DataRow dr;
            int iC = 1;
            String sql = "SELECT item*1000000+car*10000+unit*1000+prc as itt, item, SUM(cnt), prc, SUM(cnt*prc), car, unit FROM bk_list LEFT JOIN bk_bill ON bill_id=bill";
            if (sFilter != "") sql += " WHERE " + sFilter;
            sql += " GROUP BY itt";
            denSQL.denReader rd = denSQL.Query(sql);
            while (rd.Read())
            {
				BK_Item it = BK_Item.getItem(rd);
				BK_Carrier c = BK_Carrier.getCarrier(rd); // car
				Unit un = Unit.getUnit(rd); // init
                dr = dsMain.tblReport.NewRow();
                dr[0] = iC;
				dr[1] = it.NameUa + c.CaptionUa;
                dr[2] = rd.GetInt(2);
                dr[3] = rd.GetDecimal(3);
                dr[4] = rd.GetDecimal(4);
				dr[5] = un.ToString();
                dsMain.tblReport.Rows.Add(dr);
                iC++;
            }
            rd.Close();
            this.rptView.RefreshReport();
        }

        private void btnPrintClose_Click(object sender, EventArgs e)
        {
            rptView.PrintDialog();
            this.Close();
        }

        private void optDay_CheckedChanged(object sender, EventArgs e) { if(((RadioButton)sender).Checked) SetFilter(0); }

        private void optWeek_CheckedChanged(object sender, EventArgs e) { if (((RadioButton)sender).Checked) SetFilter(1); }

        private void optMonth_CheckedChanged(object sender, EventArgs e) { if (((RadioButton)sender).Checked) SetFilter(2); }

        private void optYear_CheckedChanged(object sender, EventArgs e) { if (((RadioButton)sender).Checked) SetFilter(3); }

        private void optPeriod_CheckedChanged(object sender, EventArgs e) { if (((RadioButton)sender).Checked) SetFilter(4); }

        private void SetFilter(int iFlt)
        {
            iMode = iFlt;
            switch (iFlt)
            {
                case 0: // day
                    SwitchControl(false);
                    dtFrom.Format = DateTimePickerFormat.Long;
                    lblFrom.Text = "День :";
                    break;
                case 1: // week
                    SwitchControl(false);
                    dtFrom.Format = DateTimePickerFormat.Long;
                    lblFrom.Text = "Неделя :";
                    break;
                case 2: // month
                    SwitchControl(false);
                    dtFrom.CustomFormat = "MMMM";
                    dtFrom.Format = DateTimePickerFormat.Custom;
                    lblFrom.Text = "Месяц :";
                    break;
                case 3: // year
                    SwitchControl(false);
                    dtFrom.CustomFormat = "yyyy";
                    dtFrom.Format = DateTimePickerFormat.Custom;
                    lblFrom.Text = "Год :";
                    break;
                case 4: // custom
                    SwitchControl(true);
                    dtFrom.Format = DateTimePickerFormat.Long;
                    lblFrom.Text = "С :";
                    break;
            }
            CalculateReport();
        }

        private void SwitchControl(bool bOn)
        {
            if (bOn && !lblTo.Visible)
            {
                lblTo.Visible = true;
                dtTo.Visible = true;
            }
            else if(!bOn && lblTo.Visible)
            {
                lblTo.Visible = false;
                dtTo.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
