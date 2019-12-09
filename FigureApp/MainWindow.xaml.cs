using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace FigureApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PointService _pointService { get; set; } = new PointService();
        
        FigureView FigureView { get; set; } = new FigureView();

        public MainWindow() => InitializeComponent();
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (!(bool)myDialog.ShowDialog())
                return;
            
            nameFileLabel.Content = myDialog.FileName;

            _pointService.GetDataFromFile(myDialog.FileName);

            if (!_pointService.IsCorrect)
                MessageBox.Show("Ошибка загрузки точек из файла: " + _pointService.PointStatus.ToString());
            
            FigureView.CreateFigure(_pointService.Points);

            this.UpdatePoint();
        }

        /// <summary>
        /// Изменяем маштаб фигуры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            

            var scale = e.NewValue;

            scaleFigureLabel.Content = "Размер фигуры: " + scale;

            FigureView.SetScale(scale);
            
            this.UpdatePoint();
        }

        /// <summary>
        /// Обновляем визуализацию фигуры.
        /// </summary>
        private void UpdatePoint()
        {
            if (!FigureView.IsCreate)
                return;

            PointCollection pointCollection = new PointCollection();

            foreach (var point in this.FigureView.PointsView)
                pointCollection.Add(point);

            polygonView.Points = pointCollection;
        }

        private void PolygonView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon polygon = sender as Polygon;

            var point = e.GetPosition(polygon);

            FigureView.IsSelect = true;

            FigureView.GetVectorsMouse(point);
        }

        private void PolygonView_MouseUp(object sender, MouseButtonEventArgs args)
        {
            Polygon polygon = sender as Polygon;

            FigureView.IsSelect = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas canvas = sender as Canvas;

            var position = e.GetPosition(canvas);

            if (!FigureView.IsSelect)
                return;

            var point = e.GetPosition(canvas);

            FigureView.SetPosition(point);

            this.UpdatePoint();
        }
    }
}
