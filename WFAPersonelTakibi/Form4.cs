using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using WFAPersonelTakibi.Dal;
using WFAPersonelTakibi.Models;

namespace WFAPersonelTakibi
{
    public partial class Form4 : MetroForm
    {
        private Guid id;
        ProjectContext db = new ProjectContext();

        public Form4()
        {
            InitializeComponent();
        }

        Guid _id;
        public Form4(Guid id): this()  // constructer metot olarak bunu çalıştırsın istersek
        {
            _id = id;
        }

        private void MetroLink1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
        }

        Employee employee;
        private void Form4_Load(object sender, EventArgs e)
        {
            cmbDepartment.Items.AddRange(Enum.GetNames(typeof(Department)));

            using (ProjectContext db = new ProjectContext())
            {
                employee = db.Employees.Find(_id);
                txtAddress.Text = employee.Address;
                txtFirstName.Text = employee.FirstName;
                txtLastName.Text = employee.LastName;
                txtPhone.Text = employee.Phone;
                txtMail.Text = employee.EMail;
                dtBirthDate.Value = employee.BirthDate;
                cmbDepartment.Text = employee.Department.ToString();
            }

            MetroRadioButton radioButton = (MetroRadioButton)metroPanel1.Controls.Find("rd" + 
                (employee.Gender.ToString() == "Karışık" ? "Random" : employee.Gender.ToString()), true)[0];
            radioButton.Checked = true;
            pcbImageUrl.Image = Image.FromFile($@"{Environment.CurrentDirectory}\..\..\img\{employee.ImageUrl}");
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void PcbImageUrl_Click(object sender, EventArgs e)
        {
            ofd.Filter = "jpg files (*.jpg)|*.jpg | png files (*.png)|*.png"; //herhangi bir jpg veya png dosyalarını alabiliyorz.
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pcbImageUrl.Image = Image.FromFile(ofd.FileName);

                string new_name = $@"{Guid.NewGuid()}{System.IO.Path.GetExtension(ofd.FileName)}";
                pcbImageUrl.Image.Save($@"{Environment.CurrentDirectory}\..\..\Images\{new_name}");
                pcbImageUrl.Tag = new_name;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            employee.FirstName = txtFirstName.Text;
            employee.BirthDate = dtBirthDate.Value;
            employee.LastName = txtLastName.Text;
            employee.Phone = txtPhone.Text;
            employee.EMail = txtMail.Text;
            employee.Address = txtAddress.Text;

            for (int i = 0; i < metroPanel1.Controls.Count; i++) //panelin içindeki radio butonlar kadar döner
            {
                if (metroPanel1.Controls[i] is MetroRadioButton) //eğer kontrol radiobutonsa
                {
                    MetroRadioButton metroRadio = (MetroRadioButton)metroPanel1.Controls[i]; //

                    if (metroRadio.Checked)  //çektiğimiz buton seçiliyse
                    {
                        employee.Gender = (Gender)Enum.Parse(typeof(Gender), metroRadio.Text);
                        break;
                    }
                }
            }

            employee.Department = (Department)Enum.Parse(typeof(Department), cmbDepartment.Text);

            if (pcbImageUrl.Image != null)
            {
                employee.ImageUrl = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(ofd.FileName)}";
                pcbImageUrl.Image.Save($@"{Environment.CurrentDirectory}\..\..\img\{employee.ImageUrl}");

            }

            using (ProjectContext db = new ProjectContext())
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                bool result = db.SaveChanges() > 0;
                MetroMessageBox.Show(this, result ? "Kayıt Güncellendi" : "İşlem Hatası", "Güncelleme Bildirimi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();

        }
    }
}
