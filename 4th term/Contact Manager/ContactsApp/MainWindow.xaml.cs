using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shell;
using System.Windows.Media.Animation;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App Controller;

        private bool isBold = false;
        private bool isHeighten;
        private bool isMinimized = false;

        private TaskbarItemProgressState ProgressState { get; set; }

        public MainWindow(App c)
        {
            Controller = c;
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.LoadContactList();
        }

        internal void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controller.SelectContact((int)(((sender as ListBox).SelectedItem as FrameworkElement).Tag));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddContactPanel.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddContactPanel.Visibility = Visibility.Visible;
            AddContactName.Text = "";
            AddContactName.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AddContactName.Text.Length > 0)
            {
                AddContactPanel.Visibility = Visibility.Collapsed;
                Controller.CreateContact(AddContactName.Text);
                AddContactName.Text = "";
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            this.taskBarItem.ProgressState = TaskbarItemProgressState.Error;
            this.taskBarItem.ProgressValue = 1;
            var answer = MessageBox.Show("Do U really want to delete this contact?", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
                Controller.RemoveContact();
            else
                this.taskBarItem.ProgressState = TaskbarItemProgressState.None;
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            AddDetailPanel.Visibility = System.Windows.Visibility.Visible;
            AddDetailTitle.Text = ((FrameworkElement)sender).Tag as string;
            AddDetailValue.Text = "";
            AddDetailValue.Focus();
            AddDetailPopup.IsOpen = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Controller.AddSimpleDetail(AddDetailTitle.Text, AddDetailValue.Text);
            AddDetailTitle.Text = "";
            AddDetailValue.Text = "";
            AddDetailPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddDetailTitle.Text = "";
            AddDetailValue.Text = "";
            AddDetailPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AddDetailButton_Click(object sender, RoutedEventArgs e)
        {
            AddDetailPopup.IsOpen = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.taskBarItem.ProgressState = TaskbarItemProgressState.Error;
            this.taskBarItem.ProgressValue = 1;
            var answer = MessageBox.Show("Do U want to exit application?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
                this.Close();
            else
                this.taskBarItem.ProgressState = TaskbarItemProgressState.None;
        }
        private void MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            isHeighten = true;
        }

        private void MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
          isHeighten = false;
            Rectangle rect = (Rectangle)sender;
            rect.ReleaseMouseCapture();
        }

        private void MouseMove1(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (isHeighten)
            {
                rect.CaptureMouse();
                double newH = e.GetPosition(this).Y;
                if (newH > 0) this.Height = newH;
            }
        }

        private void ThumbMinimizeButton(object sender, EventArgs e)
        {
            if (!isMinimized)
            {
                this.WindowState = WindowState.Minimized;
                isMinimized = true;
            }
            else if (isMinimized)
            {
                this.WindowState = WindowState.Normal;
                isMinimized = false;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            isMinimized = true;
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void element_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Control)sender).BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void element_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Control)sender).BorderBrush = null;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (isBold == false)
            {
                RemoveButton.FontWeight = FontWeights.Bold;
                AddButton.FontWeight = FontWeights.Bold;
                InfoButton.FontWeight = FontWeights.Bold;
                button3.FontWeight = FontWeights.Bold;
                isBold = true;
            }
            else
            {
                RemoveButton.FontWeight = FontWeights.Normal;
                AddButton.FontWeight = FontWeights.Normal;
                InfoButton.FontWeight = FontWeights.Normal;
                button3.FontWeight = FontWeights.Normal;
                isBold = false;
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.By = 120;
            da.Duration = TimeSpan.FromSeconds(1);
            da.DecelerationRatio = 1;
            da.Completed += new EventHandler(delegate
            {
                DoubleAnimation dda = new DoubleAnimation();
                dda.By = -(button7.Width -117);
                dda.Duration = TimeSpan.FromSeconds(2);
                dda.AccelerationRatio = 1;
                button7.BeginAnimation(Button.WidthProperty, dda);
            });
            button7.BeginAnimation(Button.WidthProperty, da);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
           InfoWindow infoWindow = new InfoWindow();
           infoWindow.Owner = this;
           infoWindow.ShowInfo();
        }
    }
}
