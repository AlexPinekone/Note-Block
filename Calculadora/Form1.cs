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

namespace Calculadora
{
    public partial class Form1 : Form
    {
        private string nombreArchivoAsociado = null;
        public Form1()
        {
            InitializeComponent();
            this.Text = "sin título *";
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 segundaVentana = new Form1();
            segundaVentana.Show();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                nombreArchivoAsociado = openFileDialog.FileName;
                richTextBox1.Text = File.ReadAllText(nombreArchivoAsociado);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarComo();
        }

        private void GuardarComo()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de texto|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                nombreArchivoAsociado = saveFileDialog.FileName;
                File.WriteAllText(nombreArchivoAsociado, richTextBox1.Text);
                this.Text = nombreArchivoAsociado;
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxBuscar.Visible = true;
            textBoxBuscar.Focus();

        }

        private void BuscarTexto(string textoABuscar)
        {
            int inicioBusqueda = richTextBox1.Find(textoABuscar, RichTextBoxFinds.None);
            if (inicioBusqueda != -1)
            {
                richTextBox1.Select(inicioBusqueda, textoABuscar.Length);
                richTextBox1.Focus();
            }
            else
            {
                MessageBox.Show("El texto no se encontró en el documento", "Texto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void textBoxBuscar_Leave(object sender, EventArgs e)
        {
            textBoxBuscar.Visible = false;
        }

        private void textBoxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBuscado = textBoxBuscar.Text;
                BuscarTexto(textoBuscado);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nombreArchivoAsociado))
            {
                richTextBox1.SaveFile(nombreArchivoAsociado, RichTextBoxStreamType.PlainText);
                this.Text = nombreArchivoAsociado;
            }
            else
            {
                GuardarComo();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nombreArchivoAsociado))
            {
                this.Text = "sin título *";
            }
            else
            {
                this.Text = nombreArchivoAsociado + "*";
            }
        }

        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }
    }
}
