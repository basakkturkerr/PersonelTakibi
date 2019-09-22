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
        
        public Form4 (Guid id)
        {
            this.id = id;
        }

        private void MetroLink1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            cmbDepartment.Items.AddRange(Enum.GetNames(typeof(Department)));
            Employee employee = db.Employees.Find(id);
            txtAddress.Text = employee.Address;
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            txtPhone.Text = employee.Phone;
            txtMail.Text = employee.EMail;
            dtBirthDate.Value = employee.BirthDate;
            cmbDepartment.Text = employee.Department.ToString();

        }
    }
}
