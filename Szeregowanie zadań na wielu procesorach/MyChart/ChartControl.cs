using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Szeregowanie_zadań_na_wielu_procesorach;

namespace MyChart
{
    public partial class ChartControl : Panel
    {
        public List<Szeregowanie_zadań_na_wielu_procesorach.Task> ListOfTasks = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();

        public int xLine = 0;
        public int yLine = 0;
        public float percentFrameHeight = 0.1F;
        public float percentFrameWidth = 0.1F;
        public Point LeftTopCorner;
        public Point LeftBottomCorner; 
        public Point RightBottomCorner;
        public float yScale = 0F;
        public int columnWith;
        public int columnWithAndSpace;
        public int MaxTimeOfTask;
        protected float columnWithProportion = 0.1F;
        bool IsBusy = false;
        protected int scrollBarPosition = 0;
        
        
        public ChartControl()
        {
            InitializeComponent();
        }

        protected void BeforePaint()
        {
            LeftTopCorner = new Point((int)(percentFrameWidth * this.Width), (int)(percentFrameHeight * this.Height));
            LeftBottomCorner = new Point((int)(percentFrameWidth * this.Width), (int)(this.Height - (percentFrameHeight * this.Height * 2)));
            RightBottomCorner = new Point((int)(this.Width - (percentFrameWidth * this.Width)), LeftBottomCorner.Y);
       
            xLine = RightBottomCorner.X - LeftBottomCorner.X;
            yLine = LeftBottomCorner.Y - LeftTopCorner.Y;
            columnWith = (int)((float)(xLine) * columnWithProportion);
            columnWithAndSpace = (int)((float)columnWith * 1.1F); // space between task + task 
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        

        protected override void OnPaint(PaintEventArgs pe)
        {
            BeforePaint();
            base.OnPaint(pe);
                     
            Font scaleFont = new Font(FontFamily.GenericSansSerif,8);

            Graphics graph = pe.Graphics;
            SolidBrush brush = new SolidBrush(Color.Red);
            Pen penToAdditionalLines = new Pen(brush, 1);
            Pen pen = new Pen(brush,2);
            graph.DrawRectangle(pen,new Rectangle(0,0,this.Width,this.Height));
         
            // Y and X lines 
            graph.DrawLine(pen, LeftTopCorner, LeftBottomCorner );
            graph.DrawLine(pen, LeftBottomCorner, RightBottomCorner);


            // if we have task Draw scale and help lines 
             if (ListOfTasks.Count > 0)
             {
                 // Enable / Disable ScrollBar 
                 if (GetTaskCount() * columnWith > xLine)
                 {
                     EnableHScrollBar();
                 }
                 else
                 {
                     this.scrollBarPosition = 0;
                     this.hScrollBar.Visible = false;
                 }

                 // Create horizontal lines
                 yScale = (float)yLine / (float)MaxTimeOfTask;
                 SizeF measurement = graph.MeasureString(MaxTimeOfTask.ToString(), scaleFont);
                 float scaleOftext = measurement.Height / yScale;

                 int i = 0;
                 while (LeftTopCorner.Y <= LeftBottomCorner.Y - (yScale * (i + 1)))
                 {
                     i++;
                     int yScaleI = (int)(yScale * i);

                     if (scaleOftext >= 1)
                     {
                         int tempScale = 0;


                         if ((int)scaleOftext > scaleOftext) tempScale = (int)scaleOftext;
                         else tempScale = (int)scaleOftext + 1;

                         if (i % tempScale == 0)
                         {

                             graph.DrawString(i.ToString(), scaleFont, brush, new Point(LeftBottomCorner.X - (int)measurement.Width, LeftBottomCorner.Y - yScaleI -5));
                             graph.DrawLine(penToAdditionalLines, new Point(LeftBottomCorner.X, LeftBottomCorner.Y - yScaleI), new Point(RightBottomCorner.X, LeftBottomCorner.Y - yScaleI));

                         }
                     }
                     else // if scaleOfText < 1 czyli jezeli sie mieszcza wszystkie teksty i linie 
                     {
                         graph.DrawString(i.ToString(), scaleFont, brush, new Point(LeftBottomCorner.X - (int)measurement.Width, LeftBottomCorner.Y - yScaleI -5));
                         graph.DrawLine(penToAdditionalLines, new Point(LeftBottomCorner.X, LeftBottomCorner.Y - yScaleI), new Point(RightBottomCorner.X, LeftBottomCorner.Y - yScaleI));
                     }

                 }
             }
        }

        private void EnableHScrollBar()
        {
            int scrollBarHeight = 17;
            this.hScrollBar.Location = new Point(1, this.Height - scrollBarHeight - 1);
            this.hScrollBar.Size = new Size(this.Width -2, scrollBarHeight -1);
            this.hScrollBar.Visible = true;
        }


        public void Update(List<Szeregowanie_zadań_na_wielu_procesorach.Task> lTasks)
        {
            this.ListOfTasks.Clear();
            this.ListOfTasks.AddRange(lTasks);
            this.MaxTimeOfTask = GetMaxTaskTime();
            this.Refresh();
        }
        protected int GetTaskCount()
        {
            return ListOfTasks.Count();
        }

        protected int GetMaxTaskTime()
        {
            int max = 0;
            foreach (var item in ListOfTasks)
            {
                if (item.time > max) max = item.time;
            }
            return max;
        }

        protected Color GetColorOfTask(Szeregowanie_zadań_na_wielu_procesorach.Task task)
        {
            switch(task.priority)
                         {
                             case Priority.Heigh:
                                 {
                                     return Color.OrangeRed;
                                     
                                 }
                             case Priority.Low:
                                 {
                                     return Color.Green;
                                 }
                             case Priority.Medium: 
                                 {
                                     return Color.Yellow;
                                 }
                             case Priority.VeryHeigh:
                                 {
                                     return Color.Red;
                                 }
                             case Priority.VeryLow:
                                 {
                                     return Color.SpringGreen;
                                 }
                             default:
                                 return Color.Violet;
                         }
        }


        public void SetColumnWith(int columnWith)
        {
            this.columnWithProportion = (float)columnWith / (float)xLine;
        }


        protected virtual void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

        }

        //public void AddTasks(List<ProcessorTask> lTasks)
        //{
        //    this.ListOfTasks.AddRange(lTasks);
        //    this.ListOfTasks.Sort();
        //    this.Refresh();
        //}

        //public ProcessorTask GetTask()
        //{
        //    if (!IsBusy && this.ListOfTasks.Count > 0)
        //    {
        //        IsBusy = true;
        //        ProcessorTask tempTask = this.ListOfTasks.First();
        //        this.ListOfTasks.Remove(tempTask);
        //        IsBusy = false;
        //        this.Refresh();
        //        return tempTask;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
