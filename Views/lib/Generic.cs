using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;

namespace lib
{
    public class Generic : Form
    {
        public Generic()
        { }

        public class Field
        {
            public LabelField label;
            public TextBoxField textField;

            public Field(
                Control.ControlCollection Ref,
                string Text,
                int Y,
                bool isCenterTitle = false,
                bool isPass = false
            )
            {
                const int _MarginLabel = 35;
                const int _Height = 280;
                const int _Width = 30;

                this.label = new LabelField(Text, isCenterTitle ? 120 : 10, Y);
                if (!isPass)
                {
                    this.textField = new TextBoxField(10, Y + _MarginLabel, _Height, _Width);
                }
                else
                {
                    this.textField = new TextBoxPass(10, Y + _MarginLabel, _Height, _Width);
                }

                Ref.Add(label);
                Ref.Add(textField);
            }
        }

        public class ButtonForm : Button
        {
            public ButtonForm(
                Control.ControlCollection Ref,
                string Text,
                int X,
                int Y,
                HandleButton handleAction
            )
            {
                this.Text = Text;
                this.Location = new Point(X, Y);
                this.Size = new Size(100, 30);
                this.Click += new EventHandler(handleAction);
                Ref.Add(this);
            }
        }

        public class TextBoxField : TextBox
        {

            public TextBoxField(int X, int Y, int Height, int Width)
            {
                this.Location = new Point(X, Y);
                this.Size = new Size(Height, Width);
                this.ForeColor = System.Drawing.Color.Purple;
            }
        }

        public class TextBoxPass : TextBoxField
        {
            public TextBoxPass(int X, int Y, int Height, int Width)
                : base(X, Y, Height, Width)
            {
                this.PasswordChar = '*';
            }
        }
        public class LabelField : Label
        {

            public LabelField(string Text, int X, int Y)
            {
                this.Text = Text;
                this.Location = new Point(X, Y);
            }
        }

        public class FieldOnLabel : Label
        {
            public FieldOnLabel(string Text, int x, int y, int Z, int W)
            {
                this.Text = Text;
                this.Size = new Size(x, y);
                this.Location = new Point(Z, W);
            }
        }

        public delegate void HandleButton(object sender, EventArgs e);

        public class FieldOnButton : Button
        {
            public FieldOnButton(string Text, int x, int y, int Z, int W, HandleButton handleAction)
            {
                this.Text = Text;
                this.Size = new Size(x, y);
                this.Location = new Point(Z, W);
                this.BackColor = Color.White;
                this.Click += new EventHandler(handleAction);
            }
        }

        public class FieldOnTextBox : TextBox
        {
            public FieldOnTextBox(int x, int y, int Z, int W)
            {
                this.Size = new Size(x, y);
                this.Location = new Point(Z, W);
            }
        }

        public class ViewOnFieldList : ListView
        {
            public ViewOnFieldList(int x, int y, int Z, int W)
            {
                this.Location = new Point(x, y);
                this.Size = new Size(Z, W);
            }
        }

        public class Buttons : Form
        {
            public Buttons()
            { }
            public void ClickOnReturnBttn(object sender, EventArgs e)
            {
                this.Close();
            }
        }
    }

    public class ListViewTag<T> : ListView
    {
        public ListViewTag(ControlCollection Ref, string Name, IEnumerable<T> list, string[] generics)
        {
            this.Columns.Add("Id", 100);
            foreach (string generic in generics)
            {
                this.Columns.Add(generic, 100);
            }
            foreach (T item in list)
            {
                Type type = item.GetType();
                PropertyInfo prop = type.GetProperty("Id");
                ListViewItem lvItem = new ListViewItem(prop.GetValue(item).ToString());
                foreach (string generic in generics)
                {
                    prop = type.GetProperty(generic);
                    lvItem.SubItems.Add(prop.GetValue(item).ToString());
                }
                this.Items.Add(lvItem);
            }
            this.Name = Name;
            this.Location = new System.Drawing.Point(20, 390);
            this.ClientSize = new System.Drawing.Size(250, 200);

            this.View = View.Details;
            this.LabelEdit = true;
            this.AllowColumnReorder = true;
            this.CheckBoxes = true;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Sorting = SortOrder.Ascending;

            Ref.Add(this);
        }
    }

    public class ComboCategory : ComboBox
    {
        public ComboCategory(ControlCollection Ref)
        {
            this.Text = "Selecione uma categoria...";
            this.DropDownWidth = 280;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Items.AddRange(Models.Categoria.GetCategorias().ToArray());
            this.Location = new System.Drawing.Point(10, 95);
            this.Size = new System.Drawing.Size(280, 21);
            this.DropDown += new System.EventHandler(comboDropDown);

            Ref.Add(this);
        }

        private void comboDropDown(object sender, System.EventArgs e)
        {
            MessageBox.Show("Selecione a Categoria!");
        }

    }
    public class ConfirmMessage
    {
        public static DialogResult Show
            (
                string Message =
                    "Mais um click e sua ação sera confirmada, tem certeza de que deseja isto?"
            )
        {
            return MessageBox.Show
            (
                "Confirmar",
                Message,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        }
    }
    public class CancelMessage
    {
        public static DialogResult Show
            (
                string Mensagem =
                    "Mais um click e sua ação sera cancelada, tem certeza de que deseja isto??"
            )
        {
            return MessageBox.Show(
                "Cancelar",
                Mensagem,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        }
    }

    public class ErrorMessage
    {
        public static DialogResult Show
            (
                string Mensagem = "Error... Contate o técnico responsável"
            )
        {
            return MessageBox.Show(
                "Error",
                $"Houve um erro na execução desta ação: {Mensagem}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

    }
    public enum Function
    { Create, Update }

    public class BaseForm : Form
    {
        public BaseForm(string Title)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 650);
            this.Text = Title;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
    public class ListViewItems<T> : ListView
    {
        public ListViewItems(ControlCollection Ref, string Name, IEnumerable<T> list, string[] generics)
        {
            this.Columns.Add("Id", 100);
            foreach (string generic in generics)
            {
                this.Columns.Add(generic, 100);
            }
            foreach (T item in list)
            {
                Type type = item.GetType();
                PropertyInfo prop = type.GetProperty("Id");
                ListViewItem newItem = new ListViewItem(prop.GetValue(item).ToString());
                foreach (string generic in generics)
                {
                    prop = type.GetProperty(generic);
                    newItem.SubItems.Add(prop.GetValue(item).ToString());
                }
                this.Items.Add(newItem);
            }
            this.Name = Name;
            this.Location = new System.Drawing.Point(10, 10);
            this.ClientSize = new System.Drawing.Size(280, 340);

            this.View = View.Details;
            this.LabelEdit = true;
            this.AllowColumnReorder = true;
            this.CheckBoxes = true;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Sorting = SortOrder.Ascending;

            Ref.Add(this);
        }
    }
}
