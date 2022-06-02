using System;
using System.Windows.Forms;
using lib;
using Models;
using Controllers;
using System.Drawing;

namespace Views
{
    public class UsuarioView : Form
    {
        

        
        ListView listView;
        Button bttnReturn;
        Button bttnInsert;
        Button bttnDelete;
        Button bttnUpdate;

        public UsuarioView()
        {

            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Text = "Usuário";
            
            bttnReturn = new Generic.FieldOnButton("Voltar", 355, 450, 80, 25);
			bttnReturn.Click += new EventHandler(this.ClickOnReturnBttn);
            
            bttnInsert = new Generic.FieldOnButton("Cadastrar", 15, 450, 80, 25);
            //bttnInsert.Click += new EventHandler(this.ClickOnInsertBttn);

            bttnDelete = new Generic.FieldOnButton("Deletar", 235, 450, 100, 25);
			bttnDelete.Click += new EventHandler(this.ClickOnDeleteBttn);

            bttnUpdate = new Generic.FieldOnButton("Editar", 135, 450, 80, 25);
           //bttnUpdate.Click += new EventHandler(this.ClickOnUpdateBttn);

            // Select dos registros

            listView = new Generic.ViewOnFieldList(25, 25, 450, 400);
			listView.View = View.Details;
			foreach(Usuario item in UsuarioController.VisualizarUsuario())
            {
                ListViewItem list = new ListViewItem(item.Id + "");
                list.SubItems.Add(item.Nome);	
                list.SubItems.Add(item.Email);
                listView.Items.AddRange(new ListViewItem[]{list});
            }
			listView.Columns.Add("Id", -2, HorizontalAlignment.Left);
    		listView.Columns.Add("Nome", -2, HorizontalAlignment.Left);
            listView.Columns.Add("Email", -2, HorizontalAlignment.Left);
			listView.FullRowSelect = true;
			listView.GridLines = true;
			listView.AllowColumnReorder = true;
			listView.Sorting = SortOrder.Ascending;

            this.Controls.Add(this.listView);
            this.Controls.Add(this.bttnReturn);
            this.Controls.Add(this.bttnInsert);
            this.Controls.Add(this.bttnDelete);
            this.Controls.Add(this.bttnUpdate);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Text = "Usuario";
        }

        // Funções dos botões
        public void ClickOnReturnBttn(object sender, EventArgs e)
        {
            this.Hide();
            Menu Menus = new Menu(this);
            Menus.ShowDialog();
        } 
/*
        public void ClickOnUpdateBttn(object sender, EventArgs e)
        {
            try
            {            
                if (listView.SelectedItems.Count > 0) 
                {
                    ListViewItem li = listView.SelectedItems[0];
                    EditarUsuario editUsuario = new EditarUsuario(this, Convert.ToInt32(li.Text));
                    this.Hide();
                    editUsuario.ShowDialog();
                    this.user.Close();

                }  
                else
                {
                    MessageBox.Show("Selecione um usuário para editar", "Erro");
                }                 
            }
            catch(Exception)
            {
                MessageBox.Show("Selecione um usuário para editar", "Erro");
            } 
        }

        public void ClickOnInsertBttn(object sender, EventArgs e)
        {
            CadastrarUsuário CadastrarUsuários = new CadastrarUsuário(this);
            this.Hide();
            CadastrarUsuários.ShowDialog();
            this.user.Close();
        }   
*/     
        public void ClickOnDeleteBttn(object sender, EventArgs e)
        {
        
            DialogResult result = MessageBox.Show("Deseja realmente deletar?", "Confirmar", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                try
                {            
                    if (listView.SelectedItems.Count > 0) {
                        ListViewItem li = listView.SelectedItems[0];
                        MessageBox.Show("O item de id " + li.Text + " foi deletado com sucesso", "Deletado" );
                        UsuarioController.ExcluirUsuario(Convert.ToInt32(li.Text));
                        UsuarioView usuarioView = new UsuarioView(this);
                        this.Hide();
                        usuarioView.ShowDialog();
                        this.user.Close();
                        
                    }                   
                }
                catch(Exception)
                {
                    MessageBox.Show("Erro ao deletar", "Erro");
                }
            }
        } 
        
    }

}