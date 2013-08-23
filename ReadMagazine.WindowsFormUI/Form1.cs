using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReadMagazine.Domain.Concrete.ORM;

namespace ReadMagazine.WindowsFormUI
{
    public partial class Form1 : Form
    {
        private ReadMagazineEntities _context;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var client = new Client() {
                UserName= userName.Text,
                Password= password.Text,
                Email=email.Text
            };
            
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = new ReadMagazineEntities();
        }

        
    }
}
