using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using WFAPersonelTakibi.Models;
using WFAPersonelTakibi.Dal;
using MetroFramework;

namespace WFAPersonelTakibi
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        ProjectContext db = new ProjectContext();

        private void TsmDuzenle_Click(object sender, EventArgs e)
        {

        }

        private void TsmSil_Click(object sender, EventArgs e)
        {
            Guid Id = (Guid)dgvEmployees.SelectedRows[0].Cells[0].Value;
            Employee employee = db.Employees.Find(Id);
            db.Employees.Remove(employee);
            bool result = db.SaveChanges() > 0;

            DialogResult dr = MetroMessageBox.Show(this, "Kayıt Silmek İstiyor musunuz?", "Kayıt Silme " +
                "Bildirimi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                MetroMessageBox.Show(this, $"Kayıt Silme İşlemi {(result ? "Başarılı" : "Hatalı")}", "Kayıt " +
                "Silme Bildirimi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                Liste();
            }
        }

        private void TsmYeni_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
        }

        private void TsmDetay_Click(object sender, EventArgs e)
        {

        }

        void Liste()
        {
            dgvEmployees.DataSource = db.Employees.Select(x => new
            {
                x.Id,
                Adi = x.FirstName,
                Soyadi = x.LastName,
                Mail = x.EMail,
                Telefon = x.Phone
            }).ToList();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Liste();
        }
    }
}
