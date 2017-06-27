using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MusteriTakip.UI.Model;
using MusteriTakip.UI.Utils;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;

namespace MusteriTakip.UI
{
    public partial class Musteriler : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Musteriler()
        {
            try
            {
                InitializeComponent();

                theme.LookAndFeel.SetSkinStyle(Properties.Settings.Default.Theme);

                customerBindingSource.DataSource = Service<Customer>.toBindingList;
                customerView.BestFitColumns();
            }
            catch (Exception)
            {
                
            }
        }




        private void customerView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                Customer Entity = e.Row as Customer;
                Service<Customer>.AddOrUpdate(Entity);
                customerView.RefreshData();
                customerView.BestFitColumns();
            }
            catch (Exception)
            {
                
            }
        }
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                customerView.AddNewRow();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                customerView.UpdateCurrentRow();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int index = customerView.FocusedRowHandle;
                if (index != -1)
                {
                    Customer cus = customerView.GetRow(index) as Customer;
                    Service<Customer>.Delete(cus.Id);
                    customerView.DeleteRow(customerView.FocusedRowHandle);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                customerView.ShowPrintPreview();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                customerView.PrintDialog();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = string.Format("Kaydedeceğiniz Dosya Adını Yazınız");
                sfd.InitialDirectory = Properties.Settings.Default.ExportInitialDir;
                sfd.Filter = "Excel Dosyası (*.xls)|*.xls";
                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName) && !string.IsNullOrWhiteSpace(sfd.FileName))
                {
                    Properties.Settings.Default.ExportInitialDir = Path.GetDirectoryName(sfd.FileName);
                    Properties.Settings.Default.Save();
                    customerView.ExportToXls(sfd.FileName);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void btnResfresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                customerView.RefreshData();
                customerView.BestFitColumns();
            }
            catch (Exception)
            {
                
            }
        }

        private void themeSelector_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Theme = e.Item.Tag as string;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                
            }
        }
    }
}