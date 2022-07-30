using System;
using System.IO;
using System.Text;

namespace RadiusPlot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "вывод.txt";

            Console.WriteLine("Построение G-кода для обработки сфер на токарном станке Horizon B3");
            Console.WriteLine("Необходимые данные:");
            Console.WriteLine("1. Радиус сферы ");
            Console.WriteLine("2. Радиус вершины резца");
            Console.WriteLine("3. Начальный угол сферы");
            Console.WriteLine("4. Угол между начальной и конечной точкой сферы");
            Console.WriteLine("5. Длина отрезка");
            Console.WriteLine("6. Смещение Z центра окружности");

            Console.Write("Введите радиус сферы: ");
            double spRadius = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите радиус вершины резца: ");
            double cutterRadius = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите начальный угол сферы: ");
            double angleStart = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите угол сферы: ");
            double sphereAngle = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите длину отрезка: ");
            double baseDlin = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите смещение Z центра окружности: ");
            double offset = Convert.ToDouble(Console.ReadLine());

            double radiusVector = spRadius + cutterRadius;// вычисляем радиус-вектор

            double dlinaDugi = Math.PI * radiusVector / 180.0 * sphereAngle;// вычисляем длину дуги 

            int linesQuant = Convert.ToInt32(dlinaDugi / baseDlin);//вычисляем количество линий

            double angleRad = Math.PI * angleStart / 180.0; //переводим угол в радианы

            StreamWriter writer = new StreamWriter(path, false);

                for (int i = 0; i < linesQuant; i++)
            {
                double currentRadDlin = i * baseDlin;
                
                double currentAngleCalc =  currentRadDlin / (Math.PI * radiusVector / 180);
                //Console.WriteLine(i + "текущий угол" + currentAngleCalc);
                double currentAngle = angleStart + currentAngleCalc;
               // Console.Write(currentAngle + " ");
                double currentAngleRad = Math.PI * currentAngle / 180;
               // Console.Write(currentAngleRad + " ");
                double ycur = (radiusVector * Math.Sin(currentAngleRad)) - cutterRadius; 
                double zcur = (radiusVector * Math.Cos(currentAngleRad)) - offset;
                Console.Write("y=" + ycur + " ");
               // Console.WriteLine("  " + i + "  ");
                Console.WriteLine("z=" + zcur + ";");

                writer.WriteLine("G1 " + "Y" + ycur.ToString("###.###") + " " + "Z" + zcur.ToString("###.###"));

            }

                writer.Close();






            



        }
    }
}
