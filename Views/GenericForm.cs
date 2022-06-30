using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using lib;
using Controllers;
using Models;

namespace Views
{
    // Funções de Usuário
    public class UserForm : GenericBase
    {
        public static Function option;
        public static int userId;
        public List<GenericField> generics;
        Button bttnConfirm;
        Button bttnCancel;
        public UserForm(
            Function function,
            int id = 0
        ) : base()
        {
            option = function;
            userId = id;

            Usuario usuario = null;
            if (id > 0)
            {
                usuario = UsuarioController.GetUsuario(id);
            }

            this.ClientSize = new System.Drawing.Size(345, 280);
            this.Text = function == Function.Create
                ? "Cadastro de usuário"
                : "Alteração usuário";

            base.generics.Add(new GenericField("name", 20, 30, "Nome", 300, 35, ' ', usuario != null ? usuario.Nome : null));
            base.generics.Add(new GenericField("email", 20, 90, "Email", 300, 35 , ' ', usuario != null ? usuario.Email : null));
            base.generics.Add(new GenericField("senha", 20, 150, "Senha", 300, 35, '*', usuario != null ? usuario.Senha : null));

            bttnConfirm = new Generic.FieldOnButton("Confirmar", 90, 40, 60,220, this.ClickOnConfirmBttn);
            bttnCancel = new Generic.FieldOnButton( "Cancelar", 90, 40, 180, 220, this.ClickOnCancelBttn);
           
            foreach (GenericField generic in base.generics)
            {
                this.Controls.Add(generic.label);
                this.Controls.Add(generic.textBox);
            }

            this.Controls.Add(bttnConfirm);
            this.Controls.Add(bttnCancel);
        }

        private void ClickOnConfirmBttn(object sender, EventArgs e)
        {
            GenericField genericName = base.generics.Find((GenericField generic) => generic.id == "name");
            GenericField genericEmail = base.generics.Find((GenericField generic) => generic.id == "email");
            GenericField genericSenha = base.generics.Find((GenericField generic) => generic.id == "senha");
            try
            {
                if (option == Function.Create)
                {
                    UsuarioController.InserirUsuario(
                        genericName.textBox.Text,
                        genericEmail.textBox.Text,
                        genericSenha.textBox.Text
                    );
                    MessageBox.Show("Usuário criado com sucesso");
                }
                else if (option == Function.Update)
                {
                   UsuarioController.AlterarUsuario(
                        userId,
                        genericName.textBox.Text,
                        genericEmail.textBox.Text,
                        genericSenha.textBox.Text
                    );
                    MessageBox.Show("Usuário alterado com sucesso");
                    this.Close();
                }
            }
            catch (Exception)
            {
                ErrorMessage.Show("Teste");
            }
        }

        private void ClickOnCancelBttn(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Funções de Senha   
    public class SenhaCadastrar : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        Generic.Field name;
        Generic.Field url;
        Generic.Field user;
        ListView listViewTag; 
        Generic.Field senhaEncrypt;
        Generic.Field procedure;
        ComboCategory comboCategory;
        Button bttnConfirm;
        Button bttnCancel;

        public SenhaCadastrar() : base("Cadastrar nova Senha.")
        {
            string[] fields = {"Descricao"};
            this.name = new Generic.Field(this.Controls, "Nome", 20, true);
            this.comboCategory = new ComboCategory(this.Controls);
            this.url = new Generic.Field(this.Controls, "Url", 130, true);
            this.user = new Generic.Field(this.Controls, "Usuário", 190, true);
            this.senhaEncrypt = new Generic.Field(this.Controls, "Senha", 250, true, true);
            this.procedure = new Generic.Field(this.Controls, "Procedimento", 310, true);
            this.listViewTag = new ListViewTag<Models.Tag>(this.Controls, "Lista de Tags", Models.Tag.GetTags(), fields);
            this.bttnConfirm = new Generic.ButtonForm(this.Controls, "Confirmar", 20, 600, this.ClickOnConfirmBttn);
            this.bttnCancel = new Generic.ButtonForm(this.Controls, "Voltar", 180, 600, this.ClickOnCancelBttn);
            
            this.components = new System.ComponentModel.Container();
        }

        private void ClickOnConfirmBttn(object sender, EventArgs e) 
        {
            
            try
            {
                string comboValue = this.comboCategory.Items[0].ToString(); // "1 - Nome";
                string[] comboSplit = comboValue.Split('-'); //['1 ', ' Nome']

                SenhaController.InserirSenha(
                    this.name.textField.Text,
                    Convert.ToInt16(comboSplit[0].Trim()),
                    this.url.textField.Text,
                    this.user.textField.Text,
                    this.senhaEncrypt.textField.Text,
                    this.procedure.textField.Text
                );
                MessageBox.Show("Senha cadastrada com sucesso.");
                this.Close();
                SenhaView senha = new SenhaView();
                senha.ShowDialog();
            }   
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void ClickOnCancelBttn(object sender, EventArgs e) 
        {
            this.Hide();
        }
    }
    public class SenhaAlterar : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        Generic.Field passId;
        Generic.Field name;
        Generic.Field url;
        Generic.Field user;
        ListView listViewTag; 
        Generic.Field senhaEncrypt;
        Generic.Field procedure;
        ComboCategory comboCategory;
        Button bttnConfirm;
        Button bttnCancel;

        public SenhaAlterar(string id) : base("Alterar senha existente.")
        {
            string[] fields = {"Descricao"};
            this.name = new Generic.Field(this.Controls, "Nome", 20, true);
            this.comboCategory = new ComboCategory(this.Controls);
            this.url = new Generic.Field(this.Controls, "Url", 130, true);
            this.user = new Generic.Field(this.Controls, "Usuário", 190, true);
            this.senhaEncrypt = new Generic.Field(this.Controls, "Senha", 250, true, true);
            this.procedure = new Generic.Field(this.Controls, "Procedimento", 310, true);
            this.listViewTag = new ListViewTag<Models.Tag>(this.Controls, "Lista de Tags", Models.Tag.GetTags(), fields);
            this.passId = new Generic.Field(this.Controls, "ID", 600, true);
            this.bttnConfirm = new Generic.ButtonForm(this.Controls, "Confirmar", 20, 680, this.ClickOnConfirmBttn);
            this.bttnCancel = new Generic.ButtonForm(this.Controls, "Voltar", 180, 680, this.ClickOnCancelBttn);
            
            this.components = new System.ComponentModel.Container();
        }

        private void ClickOnConfirmBttn(object sender, EventArgs e) 
        {
            try
            {
                string comboValue = this.comboCategory.Items[0].ToString(); // "1 - Nome";
                string[] comboSplit = comboValue.Split('-'); //['1 ', ' Nome']

                SenhaController.AlterarSenha(
                    Convert.ToInt16(this.passId.textField.Text),
                    this.name.textField.Text,
                    Convert.ToInt16(comboSplit[0].Trim()),
                    this.url.textField.Text,
                    this.user.textField.Text,
                    this.senhaEncrypt.textField.Text,
                    this.procedure.textField.Text
                );
                MessageBox.Show("Senha cadastrada com sucesso.");
                this.Close();
                SenhaView senha = new SenhaView();
                senha.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ClickOnCancelBttn(object sender, EventArgs e) 
        {
            this.Hide();
        }
    }

    // Funções da Tag

    public class TagForm : GenericBase
    {
        public static Function option;
        public static int tagId;
        public List<GenericField> generics;
        Button bttnConfirm;
        Button bttnCancel;
        public TagForm(
            Function function,
            int id = 0
        ) : base()
        {
            option = function;
            tagId = id;

            Tag tag = null;
            if (id > 0)
            {
                tag = TagController.GetTag(id);
            }

            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Text = function == Function.Create
                ? "Cadastro de tag"
                : "Alteração de tag";

            base.generics.Add(new GenericField("description", 10, 30, "Descrição", 280, 5, ' ', tag != null ? tag.Descricao : null));
        

            bttnConfirm = new Generic.FieldOnButton("Confirmar", 90, 35, 40, 120, this.ClickOnConfirmBttn);
            bttnCancel = new Generic.FieldOnButton( "Cancelar", 90, 35, 170, 120, this.ClickOnCancelBttn);

            foreach (GenericField generic in base.generics)
            {
                this.Controls.Add(generic.label);
                this.Controls.Add(generic.textBox);
            }

            this.Controls.Add(bttnConfirm);
            this.Controls.Add(bttnCancel);
        }

        private void ClickOnConfirmBttn(object sender, EventArgs e)
        {
            
            GenericField genericDescription = base.generics.Find((GenericField generic) => generic.id == "description");
            try
            {
                if (option == Function.Create)
                {
                    TagController.InserirTag(
                        genericDescription.textBox.Text
                    );
                    MessageBox.Show("Tag criada com sucesso");
                }
                else if (option == Function.Update)
                {
                   TagController.AlterarTag(
                        tagId,
                        genericDescription.textBox.Text
                    );
                    MessageBox.Show("Tag alterada com sucesso");
                }
            }
            catch (Exception)
            {
                ErrorMessage.Show();
            }
            this.Close();
        }

        private void ClickOnCancelBttn(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Funções de Categoria
    public class CategoryForm : GenericBase
    {
        public static Function option;
        public static int categoryId;
        public List<GenericField> generics;
        Button bttnConfirm;
        Button bttnCancel;
        public CategoryForm(
            Function function,
            int id = 0
        ) : base()
        {
            option = function;
            categoryId = id;
            
            Categoria categoria = null;
            if (id > 0) {
                categoria = CategoriaController.GetCategoria(id);
            }

            this.ClientSize = new System.Drawing.Size(300, 240);
            this.Text = function == Function.Create
                ? "Cadastro de categoria"
                : "Alteração de categoria";

            base.generics.Add(new GenericField("name", 10, 20, "Nome", 280, 15, ' ', categoria != null ? categoria.Nome : null));
            base.generics.Add(new GenericField("description", 10, 90, "Descrição", 280, 15, ' ', categoria != null ? categoria.Descricao : null));

            bttnConfirm = new Generic.FieldOnButton("Confirmar", 90, 35, 40, 170, this.ClickOnConfirmBttn);
            bttnCancel = new Generic.FieldOnButton( "Cancelar", 90, 35, 170, 170, this.ClickOnCancelBttn);

            foreach (GenericField generic in base.generics)
            {
                this.Controls.Add(generic.label);
                this.Controls.Add(generic.textBox);
            }

            this.Controls.Add(bttnConfirm);
            this.Controls.Add(bttnCancel);
        }

        private void ClickOnConfirmBttn(object sender, EventArgs e)
        {
            
            GenericField genericName = base.generics.Find((GenericField generic) => generic.id == "name");
            GenericField genericDescription = base.generics.Find((GenericField generic) => generic.id == "description");
            try
            {
                if (option == Function.Create)
                {
                    CategoriaController.InserirCategoria(
                        genericName.textBox.Text,
                        genericDescription.textBox.Text
                    );
                    MessageBox.Show("Parabéns sua categoria foi cadastrada com sucesso!", "Sucesso", MessageBoxButtons.OK);
                    this.Close();
                }
                else if (option == Function.Update)
                {
                   CategoriaController.AlterarCategoria(
                        categoryId,
                        genericName.textBox.Text,
                        genericDescription.textBox.Text
                    );
                    MessageBox.Show("Parabéns sua categoria foi alterada com sucesso!", "Sucesso", MessageBoxButtons.OK);
                    this.Close();
                }
            }
            catch (Exception)
            {
                ErrorMessage.Show();
            }
        }

        private void ClickOnCancelBttn(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    
