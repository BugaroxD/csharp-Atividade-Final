using System;
using System.Windows.Forms;
using System.Drawing;
using Models;
using Controllers;
using lib;

namespace Views
{
    public class TagView : Form
    {
        ListView listView;
        Button bttnInsert;
        Button bttnUpdate;
        Button bttnDelete;
        Button bttnReturn;

        public TagView()
        {
            this.ClientSize = new System.Drawing.Size(305, 450);
            this.Text = "Tags";

            bttnInsert = new Generic.FieldOnButton("Cadastrar", 100, 30, 35, 370, this.ClickOnInsertBttn);
            bttnUpdate = new Generic.FieldOnButton("Editar", 100, 30, 165, 370, this.ClickOnUpdateBttn);
            bttnReturn = new Generic.FieldOnButton("Voltar", 100, 30,  165, 410, this.ClickOnReturnBttn);
            bttnDelete = new Generic.FieldOnButton("Deletar", 100, 30, 35, 410, this.ClickOnDeleteBttn);

        // Select dos registros

            string[] generics = {"Descricao"};
            this.listView = new ListViewItems<Models.Tag>(this.Controls, "Lista de Tags", Models.Tag.GetTags(), generics);

            this.Controls.Add(this.bttnInsert);
            this.Controls.Add(this.bttnUpdate);
            this.Controls.Add(this.bttnReturn);
            this.Controls.Add(this.bttnDelete);
        }

        // Funções dos botões

        private void ClickOnInsertBttn(object sender, EventArgs e)
        {
            new TagForm(Function.Create).Show();
            this.Dispose();
        }

        private void ClickOnUpdateBttn(object sender, EventArgs e)
        {
            try
            {
                new TagForm(Function.Update).Show();
                this.Dispose();
            }
            catch (Exception)
            {
                ErrorMessage.Show();
            }
        }

        private void ClickOnDeleteBttn(object sender, EventArgs e)
        {
            try
            {
                int tagId = Convert.ToInt32(listView.CheckedItems[0].Text);
                DialogResult result = MessageBox.Show($"Para efetuar a exclusão da tag {tagId} clicar em SIM! Se for engano clique em NÃO!", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    TagController.ExcluirTag(tagId);
                }
                this.Close();
            }
            catch (Exception)
            {
                ErrorMessage.Show();
            }
        }

        private void ClickOnReturnBttn(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}