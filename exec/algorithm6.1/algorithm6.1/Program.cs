using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithm6._1
{
    internal class Program
    {
        static public List<Segment> OutSegmentList = new List<Segment>();

        static void Main(string[] args)
        {
            int N = 0;
            List<Segment> SegmentList = new List<Segment>();
            Console.WriteLine("[Program] Введите количество точек");
            Console.Write(">>> "); N = int.Parse(Console.ReadLine()); // Ввод количества сегментов
            Random _rnd = new Random();

            for (int i = 0; i < N; i++) // Генерация координат сегментов
            {
                int X = _rnd.Next(0, 10);
                int Y = 0;
                while (Y < X) Y = _rnd.Next(0, 10); // Координата Y не может быть меньше или равной X
                SegmentList.Add(new Segment(X, Y));
            }

            SegmentList = SegmentList.OrderBy(s => s.X).ThenBy(s => s.Y).ToList(); // Сортировка сначала по X, затем по Y
            Console.WriteLine("[Program] Отсортированный список точек:");
            foreach (Segment segment in SegmentList) Console.WriteLine(segment.WriteSegment()); // Вывод отсортированного списка
            OutSegmentList.Add(SegmentList[0]); // Добавление первого сегмента в итоговый список

            for (int i = 1; i < SegmentList.Count - 1; i++)
            {
                /*
                Проверка на то, если у последнего сегмента итогового списка равный X с текущим проверяемым сегментом,
                но у второго Y больше. Если условие выполняется, то последний сегмент итогового списка заменяется
                на текущий проверяемый сегмент входного списка
                */
                if (OutSegmentList[OutSegmentList.Count - 1].X == SegmentList[i].X && OutSegmentList[OutSegmentList.Count - 1].Y < SegmentList[i].Y)
                    OutSegmentList[OutSegmentList.Count - 1] = SegmentList[i];

                // Основная проверка 
                if (OutSegmentList[OutSegmentList.Count - 1].Y < SegmentList[i].X) OutSegmentList.Add(SegmentList[i]);
            }

            //Вывод итогового списка непересекающихся сегментов
            Console.WriteLine($"[Program] Максимальное количество не пересекающихся точек - {OutSegmentList.Count}");
            foreach (Segment segment in OutSegmentList) Console.WriteLine(segment.WriteSegment());
            Console.ReadKey();
        }
    }
}

/*
Для удобства работы с сегментами я создал класс,
в котором объект имеет координаты X, Y, а также
функцию для вывода сегмента в виде (X, Y)
 */

class Segment
{
    public int X;
    public int Y;

    public Segment(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public string WriteSegment() { return $"({X}, {Y})"; }
}