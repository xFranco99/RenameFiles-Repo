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

namespace RenameFiles {
    public partial class RenameFiles : Form {
        public RenameFiles() {
            InitializeComponent();
        }

        private void RenameFiles_Load(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

            //apre explorer
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "Seleziona la cartella dove vuoi rinominare i file";
            fbd.ShowNewFolderButton = false;

            //scrive nel textBox il path selezionato
            if (fbd.ShowDialog() == DialogResult.OK) {
                path.Text = fbd.SelectedPath;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {

            //se seleziono il checkbox dei numeri (radioButton1) devo disabilitare la TextBox2
            if (radioButton1.Checked) {
                textBox2.Enabled = false;
            } else {
                textBox2.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {

            //se non ho selezionato alcuna cartella avvisami
            if (path.Text == "") {
                MessageBox.Show("Inserisci un path!");
                return;
            }

            //prendo la dir selezionata e inizializzo a 0 un contatore
            DirectoryInfo d = new DirectoryInfo(path.Text);
            int n = 0;

            if (radioButton1.Checked) {

                //lista con tutti i file
                FileInfo[] infos = d.GetFiles();

                foreach (FileInfo f in infos) {

                    //mi salvo l'estenzione dei file
                    int lio = f.FullName.LastIndexOf('.');
                    int len = f.FullName.Length;
                    string ext = f.FullName.Substring(lio, (len - lio));

                    //rinomino file e aumento contatore
                    File.Move(f.FullName, d + @"\" + n.ToString() + ext);
                    n += 1;
                }

                // avviso fine esecuzione
                MessageBox.Show("Successo!");

            } else {
                FileInfo[] infos = d.GetFiles();

                foreach (FileInfo f in infos) {
                    int lio = f.FullName.LastIndexOf('.');
                    int len = f.FullName.Length;
                    string ext = f.FullName.Substring(lio, (len - lio));

                    //Se sono stati immessi caratteri illegali -> avvisami
                    try {
                        File.Move(f.FullName, d + @"\" + textBox2.Text.Trim() + " (" + n.ToString() + ")" + ext);
                    } catch (Exception except) {
                        MessageBox.Show($"Errore: {except.Message} \nI caratteri " + @" "" <, >, : , "", /, \, |, ?, * "" 
                            sono illegali per la creazione di file");
                        return;
                    }

                    n += 1;
                }

                MessageBox.Show("Successo!");

            }
        }
    }
}
