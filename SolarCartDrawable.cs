using Microsoft.Maui.Graphics;

namespace Solar_Mind_AI
{
    public class SolarChartDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 1. تنظيف الخلفية
            canvas.FillColor = Color.FromArgb("#F9FAFB");
            canvas.FillRectangle(dirtyRect);

            // 2. رسم المحاور
            canvas.StrokeColor = Color.FromArgb("#E5E7EB");
            canvas.StrokeSize = 1;

            float chartTop = 12;
            float chartBottom = dirtyRect.Height - 20;

            // محور الصادات
            canvas.DrawLine(38, chartTop, 38, chartBottom);
            // محور السينات
            canvas.DrawLine(38, chartBottom, dirtyRect.Width - 10, chartBottom);

            // 3. كتابة تمييز محور الصادات والأرقام
            canvas.FontColor = Color.FromArgb("#9CA3AF");
            canvas.FontSize = 8;
            canvas.DrawString("kWh", 15, chartTop - 10, 30, 15, HorizontalAlignment.Right, VerticalAlignment.Center);
            canvas.DrawString("30", 5, chartTop, 30, 15, HorizontalAlignment.Right, VerticalAlignment.Center);
            canvas.DrawString("15", 5, chartTop + ((chartBottom - chartTop) / 2) - 8, 30, 15, HorizontalAlignment.Right, VerticalAlignment.Center);
            canvas.DrawString("0", 5, chartBottom - 8, 30, 15, HorizontalAlignment.Right, VerticalAlignment.Center);

            // 4. تحديد مواضع أوقات محور السينات بدقة
            float availableWidth = dirtyRect.Width - 55;
            float x6AM = 52;
            float x12PM = 38 + (availableWidth * 0.5f); // منتصف المساحة تحت القمة مباشرة
            float x6PM = dirtyRect.Width - 22;

            // كتابة الأوقات في أماكنها الثابتة
            canvas.DrawString("6AM", x6AM - 15, chartBottom + 2, 35, 15, HorizontalAlignment.Center, VerticalAlignment.Top);
            canvas.DrawString("12PM", x12PM - 15, chartBottom + 2, 35, 15, HorizontalAlignment.Center, VerticalAlignment.Top);
            canvas.DrawString("6PM", x6PM - 15, chartBottom + 2, 35, 15, HorizontalAlignment.Center, VerticalAlignment.Top);

            // 5. نقاط الرسم البياني بحيث تكون القمة العظمى واقعة تماماً في المنتصف تحت 12PM
            float heightRange = chartBottom - chartTop;

            PointF[] points = new PointF[]
            {
                new PointF(x6AM, chartBottom - (heightRange * 0.20f)),       // 6AM: منخفض
                new PointF(38 + (availableWidth * 0.25f), chartBottom - (heightRange * 0.42f)),
                new PointF(x12PM, chartBottom - (heightRange * 0.85f)),      // 12PM: القمة في المنتصف تماماً
                new PointF(38 + (availableWidth * 0.75f), chartBottom - (heightRange * 0.40f)),
                new PointF(x6PM, chartBottom - (heightRange * 0.15f))        // 6PM: ينخفض مع الغروب
            };

            // 6. بناء مسار التدرج اللوني تحت المنحنى
            PathF chartPath = new PathF();
            chartPath.MoveTo(points[0].X, chartBottom);
            chartPath.LineTo(points[0]);

            for (int i = 0; i < points.Length - 1; i++)
            {
                var p0 = points[i];
                var p1 = points[i + 1];
                float cx = (p0.X + p1.X) / 2;
                chartPath.CurveTo(cx, p0.Y, cx, p1.Y, p1.X, p1.Y);
            }

            chartPath.LineTo(points[points.Length - 1].X, chartBottom);
            chartPath.Close();

            // تطبيق التدرج اللوني
            LinearGradientPaint gradientPaint = new LinearGradientPaint
            {
                StartColor = Color.FromArgb("#6EE7B7"),
                EndColor = Color.FromArgb("#05EFEFEF")
            };

            canvas.SetFillPaint(gradientPaint, new RectF(0, 0, dirtyRect.Width, dirtyRect.Height));
            canvas.FillPath(chartPath);

            // 7. رسم خط المنحنى الأساسي
            PathF linePath = new PathF();
            linePath.MoveTo(points[0]);
            for (int i = 0; i < points.Length - 1; i++)
            {
                var p0 = points[i];
                var p1 = points[i + 1];
                float cx = (p0.X + p1.X) / 2;
                linePath.CurveTo(cx, p0.Y, cx, p1.Y, p1.X, p1.Y);
            }

            canvas.StrokeColor = Color.FromArgb("#10B981");
            canvas.StrokeSize = 2.5f;
            canvas.DrawPath(linePath);

            // رسم النقاط الدائرية بألوان صلبة وواضحة
            foreach (var pt in points)
            {
                // 1. إطار أبيض ناصع للخارج
                canvas.SetFillPaint(null, RectF.Zero); // إلغاء أي تدرج لوني سابق
                canvas.FillColor = Colors.White;
                canvas.FillCircle(pt.X, pt.Y, 5.5f);

                // 2. نقطة خضراء صلبة من الداخل
                canvas.FillColor = Color.FromArgb("#10B981");
                canvas.FillCircle(pt.X, pt.Y, 3f);
            }
        }
    }
}