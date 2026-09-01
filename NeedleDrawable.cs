using Microsoft.Maui.Graphics;

namespace Solar_Mind_AI
{
    public class NeedleDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 1. مركز العداد الهندسي الحقيقي (منتصف العرض والارتفاع للرسمة)
            float centerX = dirtyRect.Center.X - 1f;
            float centerY = dirtyRect.Center.Y + 19; // بنزل حاجة بسيطة جداً لو القوس مرفوع لفوق

            // 2. رسم عقرب السهم من مركز العداد
            canvas.StrokeColor = Color.FromArgb("#1F2937");
            canvas.StrokeSize = 2.5f;
            canvas.StrokeLineCap = LineCap.Round;

            float needleLength = dirtyRect.Width * 0.35f; // طول السهم يتناسب تلقائياً مع حجم العداد
            float angleDeg = 18f;    // زاوية الميل لكي يشير بدقة إلى 95 على القوس الملون
            float angleRad = angleDeg * (float)Math.PI / 180f;

            float targetX = centerX + (needleLength * (float)Math.Cos(angleRad));
            float targetY = centerY - (needleLength * (float)Math.Sin(angleRad));

            // رسم خط السهم
            canvas.DrawLine(centerX, centerY, targetX, targetY);

            // 3. نقطة الارتكاز السوداء في منتصف المركز تماماً وفوق السهم
            canvas.FillColor = Color.FromArgb("#1F2937");
            canvas.FillCircle(centerX, centerY, 4.5f);
        }
    }
}