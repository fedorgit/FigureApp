using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FigureApp
{
    class PointService
    {
        public List<double> Points { get; set; }

        public PointStatusType PointStatus { get; set; }

        public bool IsCorrect { get => this.PointStatus == PointStatusType.Correct; }

        public PointService() { }

        public void GetDataFromFile(string path)
        {
            this.Points = new List<double>();

            if (!File.Exists(path))
            {
                this.PointStatus = PointStatusType.NoFile;
                return;
            }

            var readText = File.ReadAllText(path);

            var pointsText = readText.Split(' ');

            if (pointsText.Count() % 2 != 0)
            {
                this.PointStatus = PointStatusType.ErrorAmountPoint;
                return;
            }

            foreach (var point in pointsText)
            {
                if (!double.TryParse(point, out double result))
                {
                    this.PointStatus = PointStatusType.ErrorValueFormat;
                    return;
                }

                if (result < 0)
                {
                    this.PointStatus = PointStatusType.ErrorValidRange;
                    return;
                }

                this.Points.Add(result);
            }

            this.PointStatus = PointStatusType.Correct;
        }

    }
}
