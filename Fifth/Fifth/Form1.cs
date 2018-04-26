using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Fifth {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            //Series series2;
            //chart1.Series["Series2"].ChartType = SeriesChartType.Line;
            //chart1.Series["Series2"].Points.AddY(20);
            //chart1.Series["Series2"].Points.AddY(30);
            //chart1.Series["Series2"].Points.AddY(40);


            List<Point> points = new List<Point>();
            List<Pair> pairs = new List<Pair>();


            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\triangle_north.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\circle.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\triangle_sw.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\triangle_ne.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\square.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\square_vert.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\square_diamond.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\square_diamond_squashed.png");
            //Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\fourpointstar.png");
            Bitmap img = new Bitmap(@"C:\Users\riwelser\Documents\test_pics\dancer_outline.png");
            for (int i = 0; i < img.Width; i++) {
                for (int j = 0; j < img.Height; j++) {
                    Color pixel = img.GetPixel(i, j);
                    int h = img.Height;
                    int w = img.Width;
                    int x = i + 1;
                    int y = (h - j);
                    String htmlColor = System.Drawing.ColorTranslator.ToHtml(pixel);


                    if (htmlColor != "#FFFFFF") {
                        //Debug.WriteLine(htmlColor);
                        //Debug.WriteLine(x);
                        //Debug.WriteLine(y);
                        Point p = new Point(x, y);
                        points.Add(p);
                    }
                }
            }

            ////////////////////////////

            for (int i = 0; i < points.Count; i++) {
                for (int j = 0; j < points.Count; j++) {
                    if (!points[i].Equals(points[j])) {
                        var pp = new Pair(points[i], points[j]);
                        bool alreadyHas = false;
                        for (int k = 0; k < pairs.Count; k++) {
                            if (pairs[k].Equals(pp)) {
                                alreadyHas = true;
                            }
                        }
                        if (!alreadyHas) {
                            pairs.Add(pp);
                            //Debug.WriteLine(pp.ns_dist);
                        }
                    }
                }
            }

            Debug.WriteLine(points.Count);
            Debug.WriteLine(pairs.Count);


            var series = new Series("Finance") {
                ChartType = SeriesChartType.Line
            };

            double[] dists = new double[pairs.Count];
            int[] pairInts = new int[pairs.Count];

            for (int i = 0; i < pairs.Count; i++) {
                dists[i] = pairs[i].ns_dist;
                pairInts[i] = i;
            }
            //Array.Sort(dists);

            // Frist parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(pairInts, dists);
            chart1.Series.Add(series);

            ///////////////////////////////////

            //int mean_y = 0;
            //int mean_x = 0;
            //for (int i = 0; i < points.Count; i++) {
            //    mean_x += points[i].x;
            //    mean_y += points[i].y;
            //}

            //Point c = new Point(mean_x / points.Count, mean_y / points.Count);

            //for (int i = 0; i < points.Count; i++) {
            //    points[i].SetNSDistCenter(c);
            //}

            //var series = new Series("Finance") {
            //    ChartType = SeriesChartType.Line
            //};

            //double[] dists = new double[points.Count];
            //int[] pairInts = new int[points.Count];

            //for (int i = 0; i < points.Count; i++) {
            //    dists[i] = points[i].ns_dist_center;
            //    pairInts[i] = i;
            //}
            ////Array.Sort(dists);

            //// Frist parameter is X-Axis and Second is Collection of Y- Axis
            //series.Points.DataBindXY(pairInts, dists);
            //chart1.Series.Add(series);


            //using (StreamWriter writetext = new StreamWriter(@"C:\Users\riwelser\Documents\test_pics\test.csv")) {
            //    for (int i = 0; i < dists.Length; i++) {
            //        writetext.WriteLine(pairInts[i] + "," + dists[i]);
            //    }
            //}

        }

}

    class Pair {

        public Point p1, p2;
        public Double ns_dist, s_dist;

        public Pair(Point p1, Point p2) {
            this.p1 = p1;
            this.p2 = p2;
            SetDist(p1, p2);
        }

        public void SetDist(Point p1, Point p2) {
            Double dist = Math.Round(Math.Sqrt(Math.Pow((p1.x - p2.x), 2) + Math.Pow((p1.y - p2.y), 2)), 2);
            ns_dist = Math.Abs(dist);
            s_dist = dist;
        }

        public bool Equals(Pair p) {
            if ((p.p1 == p1 || p.p1 == p2) && (p.p2 == p1 || p.p2 == p2)) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    class Point {
        public int x, y;
        public double ns_dist_center;
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
            
        }

        public Point(int x, int y, Point center) {
            this.x = x;
            this.y = y;
            SetNSDistCenter(center);
        }

        public void SetNSDistCenter(Point c) {
            Double dist = Math.Round(Math.Sqrt(Math.Pow((x - c.x), 2) + Math.Pow((y - c.y), 2)), 2);
            ns_dist_center = Math.Abs(dist);
        }

        public bool Equals(Point p) {
            return (x == p.x && y == p.y);
        }

        public String Print() {
            String text = "{" + x + ", " + y + "}";
            return text;
        }
    }

}
