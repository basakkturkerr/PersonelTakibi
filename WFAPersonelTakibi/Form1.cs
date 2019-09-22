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
    //using Dal;
    //using Models;
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProjectContext db = new ProjectContext();

        private void MetroLink1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog(); //showdan farkı -> açıksa form kapanmadan başkasını açamaz.
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.FirstName = txtFirstName.Text;
            employee.BirthDate = dtBirthDate.Value;
            employee.LastName = txtLastName.Text;
            employee.Phone = txtPhone.Text;
            employee.EMail = txtMail.Text;
            employee.Address = txtAddress.Text;
            
            for(int i = 0; i < metroPanel1.Controls.Count; i++) //panelin içindeki radio butonlar kadar döner
            {
                if (metroPanel1.Controls[i] is MetroRadioButton) //eğer kontrol radiobutonsa
                {
                    MetroRadioButton metroRadio = (MetroRadioButton)metroPanel1.Controls[i]; //
                    if (metroRadio.Checked)  //çektiğimiz buton seçiliyse
                    {
                        employee.Gender = (Gender)Enum.Parse(typeof(Gender), metroRadio.Text);
                        //enum.Parse
                        //enum.GetName
                        //enum.GetNames
                        //gender içindeki enum tipini text yapar.
                        //"kest" işlemi convert sınıfının destek vermediği yerlerde kullanılır.
                        //Dönüştürme işlemi yapmaz. Sen gendersin der gönderir. Değerin tipinden yüzde yüz eminsek kullanırız.
                        break;
                    }
                }
            }

            employee.Department = (Department)Enum.Parse(typeof(Department), cmbDepartment.Text);

            //kullanıcı bir resim seçtiyse, seçilen resmi profil resmi yap.
            if (pcbImageUrl.Image != null)
            {
                employee.ImageUrl = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(ofd.FileName)}";
                //ofd.Filename tüm uzatıyı getirir. Sadece uzantısını (jpg or png) almak için bu şekilde yazdık.

                pcbImageUrl.Image.Save($@"{Environment.CurrentDirectory}\..\..\img\{employee.ImageUrl}");

                //Environment.CurrentDirectory programın exe dosyasının yolunu teslim eder.
            }

            db.Employees.Add(employee);
            //db.SaveChanges();
            bool result = db.SaveChanges() > 0; //kaç satır etkilendiyse 
            MetroMessageBox.Show(this, $"Kayıt ekleme işlemi {(result ? "Başarılı" : "Hatalı")}", "Kayıt Ekleme " +
                "Bildirimi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbDepartment.Items.AddRange(Enum.GetNames(typeof(Department)));
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void PcbImageUrl_Click(object sender, EventArgs e)
        {
            ofd.Filter = "jpg files (*.jpg)|*.jpg | png files (*.png)|*.png"; //herhangi bir jpg veya png dosyalarını alabiliyorz.
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pcbImageUrl.Image = Image.FromFile(ofd.FileName);
            }
        }
    }
}
