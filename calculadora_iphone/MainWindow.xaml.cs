using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculadora_iphone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            dibujarTeclado();
        }
        public void dibujarTeclado()
        {
            List<string> num = new List<string> { "AC", "%", "OFF", "/", "7", "8", "9", "X", "4", "5", "6", "-", "3", "2", "1", "+", "0", ",", "", "=" };
            ImageBrush btn = new ImageBrush();
            btn.ImageSource = new BitmapImage(new Uri("./Resource/btnAmarillo.png", System.UriKind.RelativeOrAbsolute));
            int columna = 0;
            int fila = 0;
            int contado = 0;

            foreach (string str in num)
            {

                Button tecla = new Button();
                tecla.Width = 55;
                tecla.Height = 48;
                tecla.Content = str;

                if (contado < 3)
                {
                    tecla.Background = Brushes.Gray;
                    contado++;
                }
                else if (contado == 3
                    || contado == 7 || contado == 11 || contado == 15 || contado == 19)
                {
                    tecla.Background = Brushes.Orange;
                    contado++;
                }
                else
                {
                    tecla.Background = btn;
                    contado++;
                }
                tecla.BorderThickness = new Thickness(0);
                tecla.Click += Tecla_Click;

                Grid.SetColumn(tecla, columna);
                Grid.SetRow(tecla, fila);
                Teclado.Children.Add(tecla);
                columna++;

                if (columna == 4)
                {
                    columna = 0;
                    fila++;
                }
            }
        }



        private double result;

        private void Tecla_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num;
                string valorAnterior = txt3.Text;
                Button btn = (Button)sender;
                if (btn.Content.ToString() == ",")
                {
                    txt3.Text += btn.Content.ToString();
                }
                else if (int.TryParse(btn.Content.ToString(), out num))
                {
                    //si es numero
                    if (int.TryParse(valorAnterior, out num))
                    {
                        txt3.Text += btn.Content.ToString();
                    }
                    else if (valorAnterior == "+" || valorAnterior == "-" || valorAnterior == "X" || valorAnterior == "/" || valorAnterior == "%")
                    {
                        txt3.Text = btn.Content.ToString();
                    }
                    else if (!int.TryParse(valorAnterior, out num) && !int.TryParse(btn.Content.ToString(), out num))
                    {   //si el valor anterior y el valor actual del boton no son numeros
                        txt3.Text = "";
                    }
                    else if (txt1.Text == "")
                    {
                        txt3.Text += btn.Content.ToString();
                        valorAnterior = txt3.Text;
                        txt1.Text = valorAnterior;
                    }
                    else
                    {
                        txt3.Text += btn.Content.ToString();
                    }
                }
                else
                {   //si el valor no es un numero
                    txt3.Text = btn.Content.ToString();
                }
                if (btn.Content.ToString() == "AC")
                {
                    txt1.Text = "";
                    txt2.Text = "";
                    txt3.Text = "";
                }
                else if (btn.Content.ToString() == "%")
                {
                    txt1.Text = valorAnterior;
                    txt2.Text = "%";

                }
                else if (btn.Content.ToString() == "X")
                {
                    txt1.Text = valorAnterior;
                    txt2.Text = "X";
                }
                else if (btn.Content.ToString() == "/")
                {
                    txt1.Text = valorAnterior;
                    txt2.Text = "/";
                }
                else if (btn.Content.ToString() == "+")
                {
                    txt1.Text = valorAnterior;
                    txt2.Text = "+";
                }
                else if (btn.Content.ToString() == "-")
                {
                    txt1.Text = valorAnterior;
                    txt2.Text = "-";
                }
                else if (btn.Content.ToString() == "=")
                {

                    if (txt2.Text == "%")
                    {

                        result = (Convert.ToDouble(txt1.Text)) % (Convert.ToDouble(valorAnterior));
                        txt1.Text = $"{txt1.Text} % {valorAnterior}";
                        txt3.Text = result.ToString();
                        txt2.Text = "=";
                    }
                    else if (txt2.Text == "X")
                    {
                        result = Convert.ToDouble(txt1.Text) * Convert.ToDouble(valorAnterior);
                        txt1.Text = $"{txt1.Text} x {valorAnterior}";
                        txt3.Text = result.ToString();
                        txt2.Text = "=";
                    }
                    else if (txt2.Text == "/")
                    {
                        result = Convert.ToDouble(txt1.Text) / Convert.ToDouble(valorAnterior);
                        txt1.Text = $"{txt1.Text} / {valorAnterior}";
                        txt3.Text = result.ToString();
                        txt2.Text = "=";
                    }
                    else if (txt2.Text == "+")
                    {
                        result = Convert.ToDouble(txt1.Text) + Convert.ToDouble(valorAnterior);
                        txt1.Text = $"{txt1.Text} + {valorAnterior}";
                        txt3.Text = result.ToString();
                        txt2.Text = "=";
                    }
                    else if (txt2.Text == "-")
                    {
                        result = Convert.ToDouble(txt1.Text) - Convert.ToDouble(valorAnterior);
                        txt1.Text = $"{txt1.Text} - {valorAnterior}";
                        txt3.Text = result.ToString();
                        txt2.Text = "=";
                    }
                }
                else if (btn.Content.ToString() == "OFF")
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
    }
}