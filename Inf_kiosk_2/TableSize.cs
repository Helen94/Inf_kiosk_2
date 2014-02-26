using System;

namespace Inf_kiosk_2
{
    class TableSize
    {

        public void NumberCell(int n, out int colCount, out int rowCount)
        {
            colCount = (int)Math.Floor(Math.Sqrt(n * 4d / 3d));
            rowCount = (int)Math.Ceiling((double)n / colCount);
            if (n != 3) return;
            colCount = 1;
            rowCount = 3;
        }
    }
}
