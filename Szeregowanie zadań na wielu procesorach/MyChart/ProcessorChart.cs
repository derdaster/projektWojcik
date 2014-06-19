using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyChart
{
    class ProcessorChart:ChartControl, IRoleable
    {
        SolidBrush textBrush = new SolidBrush(Color.Black);

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //pe.Graphics.DrawString(LeftBottomCorner.X.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 12), textBrush, LeftBottomCorner.X, LeftBottomCorner.Y + 10);
            //pe.Graphics.DrawString(RightBottomCorner.X.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 12), textBrush, RightBottomCorner.X, LeftBottomCorner.Y + 10);
            DrawTask(pe.Graphics);
        }

        #region IRoleable Members

        public void DrawTask(Graphics graph)
        {
            if (ListOfTasks.Count > 0)
            {
                Szeregowanie_zadań_na_wielu_procesorach.Task myTask = ListOfTasks[0];

                int yLeftTopPoint = LeftBottomCorner.Y - (int)((float)myTask.getTimeLeft() * yScale);
                SolidBrush fillOfTaskBrush = new SolidBrush(GetColorOfTask(myTask));

                int xCenter = (RightBottomCorner.X - LeftBottomCorner.X) / 2;

                graph.FillRectangle(fillOfTaskBrush, new Rectangle(LeftBottomCorner.X + xCenter - columnWith/2, yLeftTopPoint, this.columnWith, LeftBottomCorner.Y - yLeftTopPoint));
                graph.DrawString(myTask.timeLeft.ToString(), new System.Drawing.Font(FontFamily.GenericSerif, 12), textBrush, LeftBottomCorner.X + xCenter, LeftBottomCorner.Y + 10);

            }

            //foreach (var task in ListOfTasks)
            //{

            //    int yLeftTopPoint = LeftBottomCorner.Y - (int)((float)task.time * yScale);

            //    SolidBrush fillOfTaskBrush = new SolidBrush(GetColorOfTask(task));

            //    graph.FillRectangle(fillOfTaskBrush, new Rectangle(LeftBottomCorner.X + 2 + interval, yLeftTopPoint -2, columnWith, LeftBottomCorner.Y - yLeftTopPoint));
            //    interval += columnWithAndSpace;
            //}
        }

        #endregion
    }
}
