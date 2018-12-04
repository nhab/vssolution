//by nhab at github based on an answer of https://stackoverflow.com/users/2338477/tarmopikaro in https://stackoverflow.com/questions/707107/parsing-visual-studio-solution-files
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vssolution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                string projectFile = "";
                ofd.Filter = "*.sln|*.sln;*.sln";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                    projectFile = ofd.FileName;
                else
                    return;
                
                String outProjectFile = Path.Combine(Path.GetDirectoryName(projectFile), Path.GetFileNameWithoutExtension(projectFile) + "_2.sln");
                Solution s = new Solution(projectFile);
                foreach (var proj in s.GetProjects())
                {
                    ListViewItem lvItm = new ListViewItem(proj.ProjectName);
                    ListViewItem.ListViewSubItem lvsub=new ListViewItem.ListViewSubItem();
                    lvsub.Text = proj.RelativePath;
                    ListViewItem.ListViewSubItem lvsub1 = new ListViewItem.ListViewSubItem();
                    lvsub1.Text = proj.ProjectGuid;
                    lvItm.SubItems.Add(lvsub);
                    lvItm.SubItems.Add(lvsub1);
                    listView1.Items.Add(lvItm);
                 }

                SolutionProject p = s.GetProjects().Where(x => x.ProjectName.Contains("Plugin")).First();
                p.RelativePath = Path.Combine(Path.GetDirectoryName(p.RelativePath), Path.GetFileNameWithoutExtension(p.RelativePath) + "_Variation" + ".csproj");
                //textBox1.Text = str+"\r\n"+p.RelativePath;
              //  s.SaveAs(outProjectFile);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

    }
}
