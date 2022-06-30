using System;
using System.Windows.Forms;
using System.Drawing;
using Models;
using Controllers;
using lib;

namespace Views
{
    public class SenhaView : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        ListView listaSenhas;
        Button bttnInsert;
        Button bttnUpdate;
        Button bttnDelete;
        Button bttnReturn;

        public SenhaView() : base("Senhas")
        {
            string[] fields = { "Nome", "CategoriaId", "Url", "Usuario", "SenhaEncrypt", "Procedimento" };
            this.listaSenhas = new ListViewItems<Models.Senha>(this.Controls, "Lista de Senhas", Models.Senha.GetSenhas(), fields);
            this.bttnInsert = new Generic.ButtonForm(this.Controls, "Cadastrar", 35, 370, this.ClickOnInsertBttn);
            this.bttnUpdate = new Generic.ButtonForm(this.Controls, "Editar", 165, 370, this.ClickOnUpdateBttn);
            this.bttnDelete = new Generic.ButtonForm(this.Controls, "Deletar", 35, 410, this.ClickOnDeleteBttn);
            this.bttnReturn = new Generic.ButtonForm(this.Controls, "Voltar", 165, 410, this.ClickOnReturnBttn);

            this.components = new System.ComponentModel.Container();
        }

        // Funções dos botões

        private void ClickOnInsertBttn(object sender, EventArgs e)
        {
            SenhaCadastrar novaSenha = new SenhaCadastrar();
            novaSenha.ShowDialog();
        }

        private void ClickOnUpdateBttn(object sender, EventArgs e)
        {
            try
            {
                SenhaAlterar alterarSenha = new SenhaAlterar(this.listaSenhas.CheckedItems[0].Text);
                alterarSenha.ShowDialog();
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
                SenhaController.ExcluirSenha(Convert.ToInt32(listaSenhas.CheckedItems[0].Text));
                MessageBox.Show("Senha removida com sucesso.");
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
