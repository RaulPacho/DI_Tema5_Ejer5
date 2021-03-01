using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema5_Ejer5
{
    public enum Tipo
    {
        NUMERICO,
        TEXTUAL
    }
    public partial class ValidateTextBox : UserControl
    {
        Tipo t = Tipo.TEXTUAL;
        bool taBien = false;
        int ancho, alto;
        [Category("Cosas")]
        [Description("Pa cuando cambie el textBox")]
        public event System.EventHandler TextChanges;

        public Tipo T
        {
            get
            {
                return t;
            }
            set
            {
                this.t = value;
                textBox1_TextChanged(this, EventArgs.Empty);
            }
        }
        
        public string TextTxt
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
                TextChanges?.Invoke(this, EventArgs.Empty);
                this.Refresh();
            }
        }

        

        public bool Multiline
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
                this.Refresh();
            }
        }
        public ValidateTextBox()
        {
            InitializeComponent();
            this.Text = "";
        }

        public void darTamanos()
        {
            textBox1.Location = new Point(10, 10);
            this.Height = textBox1.Height + 20;
           
            textBox1.Width = this.Width - 20;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ancho = this.Width;
            alto = this.Height;
            darTamanos();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen p = taBien? new Pen(Color.Green): new Pen(Color.Red);
            g.DrawRectangle(p,new Rectangle(new Point(5, 5),new Size(this.Width - 10, this.Height - 10)));
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            textBox1.Text += "";
        }
        protected override void OnTextChanged(EventArgs e)
        {
            textBox1_TextChanged(this, e);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (t == Tipo.NUMERICO)
            {
                try
                {
                    int.Parse(textBox1.Text.Trim());
                    taBien = true;
                }
                catch (FormatException)
                {
                    taBien = false;
                }

            }
            else
            {
                bool algoNoCuadra = true;
                foreach(char c in textBox1.Text.ToUpper())
                {
                    if(c >= 'A' && c <= 'Z')
                    {
                        algoNoCuadra = false;
                    }
                    if(c == ' ')
                    {
                        algoNoCuadra = false;
                    }

                    if (algoNoCuadra)
                    {
                        break;
                    }
                }

                taBien = !algoNoCuadra;
            }
            this.Refresh();
            
        }
    }
}
