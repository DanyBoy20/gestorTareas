using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeTareas
{
    public partial class FMain : Form
    {
        private bool isNewTask;
        private bool hasChanges;

        public FMain()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            this.LstTask.Enabled = false;
            this.BtnDelete.Enabled = false;
            this.BtnSave.Enabled = false;
            this.BtnCancel.Enabled = false;
            this.TxtTask.Enabled = false;
            this.BtnAdd.Enabled = true;
            this.TxtTask.Text = "";

            // activamos/desactivamos el listbox dependiendo si hay valores
            this.LstTask.Enabled = this.LstTask.Items.Count > 0;
            this.LstTask.SelectedIndex = -1;

            hasChanges = false;
        }

        private void AddNewTask()
        {
            if (hasChanges)
            {
                if (MessageBox.Show("¿Guardar cambios?", "GUARDAR",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!this.SaveChanges())
                    {
                        return;
                    }
                }
            }

            this.BtnSave.Enabled = true;
            this.BtnCancel.Enabled = true;
            this.BtnAdd.Enabled = false;
            this.TxtTask.Enabled = true;
            this.TxtTask.Text = "";
            this.TxtTask.Focus();

            isNewTask = true;
        }

        private void DeleteTask()
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el elemento?", "CONFIRMAR ELIMINACION", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (LstTask.SelectedIndex >= 0 && LstTask.SelectedIndex < LstTask.Items.Count)
                {
                    this.LstTask.Items.RemoveAt(this.LstTask.SelectedIndex);
                    this.Reset();
                }
            }                           
        }

        private bool SaveChanges()
        {
            if (TxtTask.Text.Length == 0)
            {
                MessageBox.Show("Debe escribir un nombre para la tarea", "Guardar", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (isNewTask)
            {
                this.LstTask.Items.Add(this.TxtTask.Text);
                this.Reset();
            }
            else
            {
                this.LstTask.Items[this.LstTask.SelectedIndex] = this.TxtTask.Text;
                MessageBox.Show("Guardado correctamente");
            }
            return true;
        }

        private void Cancel()
        {
            if (hasChanges)
            {
                if (MessageBox.Show("¿Guardar cambios?", "GUARDAR",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!this.SaveChanges())
                    {
                        return;
                    }
                }
            }
            this.Reset();
        }

        private void LoadSelectedTask()
        {
            if (LstTask.SelectedIndex >= 0 && LstTask.SelectedIndex < LstTask.Items.Count)
            {
                this.TxtTask.Text = LstTask.Items[LstTask.SelectedIndex].ToString();

                this.BtnSave.Enabled = true;
                this.BtnDelete.Enabled = true;
                this.TxtTask.Enabled = true;
                this.BtnCancel.Enabled = true;

                isNewTask = false;
            }
            
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            this.AddNewTask();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteTask();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Cancel();
        }

        private void LstTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSelectedTask();
        }

        private void TxtTask_TextChanged(object sender, EventArgs e)
        {
            hasChanges = true;
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Guardar cambios?", "GUARDAR",
               MessageBoxButtons.YesNoCancel);
            if (resultado == DialogResult.Yes)
            {
                if (!this.SaveChanges())
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (resultado == DialogResult.No)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
