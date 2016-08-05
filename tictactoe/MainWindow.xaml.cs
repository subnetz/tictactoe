using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tictactoe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 
    /*
     * Possible approches to analyze if a player won:
     * -every time a player sets a sign all possible winning spots are tested
     * 
     * 
     */

    public partial class MainWindow : Window
    {
        bool Osturn = true;
        Button[] buttons = new Button[9];
        List<short> xpos;
        List<short> opos;
        bool haswinner = false;
        short[] winval = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 3, 6, 1, 4, 7, 2, 5, 8, 0, 4, 8, 2, 4, 6 };

        public MainWindow()
        {
            InitializeComponent();
            loadButtons();
        }

        private void loadButtons()
        {
            int column = 0, row = 1;

            for(int i = 0; i < buttons.Length; i++)
            {
                if (column == 3)
                {
                    column = 0;
                    row++;
                }
                buttons[i] = new Button();
                buttons[i].Width = 133;
                buttons[i].Height = 133;
                buttons[i].FontSize = 60;
                buttons[i].Name = "Button" + i;
                buttons[i].Tag = i;
                buttons[i].Click += Button_Click;
                Grid.SetColumn(buttons[i], column);
                Grid.SetRow(buttons[i], row);
                boardgrid.Children.Add(buttons[i]);
                ++column;
                
            }
            xpos = new List<short>();
            opos = new List<short>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Content == null)
            {
                if (Osturn)
                {
                    b.Content = "O";
                    opos.Add((short)int.Parse(b.Tag.ToString()));
                }else
                {
                    b.Content = "X";
                    xpos.Add((short)int.Parse(b.Tag.ToString()));
                }
                Osturn = !Osturn;
                if (opos.Count > 2 || xpos.Count > 2)
                {
                    Analyse();
                }
            }
            
        }


        private bool check(short val1,short val2,short val3)
        {
            List<short> val = Osturn ? xpos : opos;
            if (val.Contains(val1) && val.Contains(val2) && val.Contains(val3))
                return true;
            return false;
        }

        private void Analyse()
        {
            for(int i = 0; i < 24; i+=3)
            {
                if (check(winval[i], winval[i + 1], winval[i + 2])){
                    haswinner = true;
                    buttons[winval[i]].Foreground = new SolidColorBrush(Colors.Red);
                    buttons[winval[i+1]].Foreground = new SolidColorBrush(Colors.Red);
                    buttons[winval[i+2]].Foreground = new SolidColorBrush(Colors.Red);

                    if (Osturn)
                    {
                        MessageBox.Show("X won");
                    }else
                    {
                        MessageBox.Show("O won");
                    }
                }
            }
            if (opos.Count + xpos.Count == 9 && !haswinner)
            {
                MessageBox.Show("It's a draw");
            }

        }

        private void MenuItem_ClickClose(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void MenuItem_ClickNewGame(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        private void setBeginner()
        {
            MessageBoxResult res= MessageBox.Show("Soll X Beginnen?", "Beginnen", MessageBoxButton.YesNo);
            if (res.Equals(MessageBoxResult.Yes))
            {
                Osturn = false;
            }else
            {
                Osturn = true;
            }
        }

        private void newGame()
        {
            loadButtons();
            setBeginner();
            haswinner = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setBeginner();
        }
    }
}
