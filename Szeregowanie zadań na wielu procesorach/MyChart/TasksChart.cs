using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyChart
{
    class TasksChart: ChartControl, IRoleable
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawTask(pe.Graphics);

        }
        #region IRoleable Members

        public void DrawTask(System.Drawing.Graphics graph)
        {
            this.hScrollBar.Maximum = ListOfTasks.Count + ListOfTasks.Count /2  ;

            // Create Rectangle for each task 
            int interval = 0;
            int lCounter = 0;
            SolidBrush textBrush = new SolidBrush(Color.Black);

            foreach (var task in ListOfTasks)
            {
                int yLeftTopPoint = LeftBottomCorner.Y - (int)((float)task.time * yScale);
                SolidBrush fillOfTaskBrush = new SolidBrush(GetColorOfTask(task));

                int stepToscroll = (columnWithAndSpace / 2);
                // draw after LeftBottomCorner.X
                if (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll >= LeftBottomCorner.X) //+ scrollBarPosition * stepToscroll)
                {
                    if (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll + columnWith < LeftBottomCorner.X + xLine)
                    {
                        graph.FillRectangle(fillOfTaskBrush, new Rectangle(LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll, yLeftTopPoint, columnWith, LeftBottomCorner.Y - yLeftTopPoint));
                        graph.DrawString(task.time.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 12), textBrush, new PointF(LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll + columnWith / 2, LeftBottomCorner.Y + 10));
                    }
                    else
                    {
                        // draw part of last element
                        if (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll < LeftBottomCorner.X + xLine)
                        {
                            graph.FillRectangle(fillOfTaskBrush, new Rectangle(LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll, yLeftTopPoint, (RightBottomCorner.X - (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll)), LeftBottomCorner.Y - yLeftTopPoint));
                            graph.DrawString(task.time.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 6), textBrush, new PointF(RightBottomCorner.X, LeftBottomCorner.Y + 10));
                        }
                    
                    }
                }
                else
                {
                    // draw part of first elelemnt 
                    if (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll + columnWith > LeftBottomCorner.X)
                    {
                        graph.FillRectangle(fillOfTaskBrush, new Rectangle(LeftBottomCorner.X , yLeftTopPoint, columnWith - (LeftBottomCorner.X - (LeftBottomCorner.X + interval - scrollBarPosition * stepToscroll)), LeftBottomCorner.Y - yLeftTopPoint));
                        graph.DrawString(task.time.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 6), textBrush, new PointF(LeftBottomCorner.X, LeftBottomCorner.Y + 10));
                    }

                }
                interval += columnWithAndSpace;
                
                lCounter++;
            }
        }


        private void InvalidateTasks()
        {
            int interval =0;
            foreach (var task in ListOfTasks)
            {
                if (scrollBarPosition < LeftBottomCorner.X + interval)
                {
                    if (LeftBottomCorner.X + interval + columnWith < xLine + scrollBarPosition)
                    {
                        int yLeftTopPoint = LeftBottomCorner.Y - (int)((float)task.time * yScale);

                        Invalidate(new Rectangle(LeftBottomCorner.X + interval - scrollBarPosition, yLeftTopPoint, columnWith, LeftBottomCorner.Y - yLeftTopPoint));
                        interval += columnWithAndSpace;
                    }
                }
            }
        }


        protected override void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue > e.NewValue)
            {
                if (this.scrollBarPosition > 0)
                {
                    this.scrollBarPosition--;
                }
            }
            else if (e.OldValue < e.NewValue)
            {
                if (this.scrollBarPosition < this.hScrollBar.Maximum)
                {
                   this.scrollBarPosition++;
                }
            }
            
        }

        #endregion
    }
}
