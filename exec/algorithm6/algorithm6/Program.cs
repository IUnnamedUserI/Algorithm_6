using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int N = 10;
        List<Segment> SegmentList = new List<Segment>();
        Random _rnd = new Random();
        Console.WriteLine("[Program] Сгенерированные точки:");
        for (int i = 0; i < N; i++)
        {
            int X = _rnd.Next(0, 9);
            int Y = 0;
            while (Y < X) Y = _rnd.Next(0, 9);
            SegmentList.Add(new Segment(X, Y));
            Console.WriteLine($"[{i}] {SegmentList[i].WriteSegment()}");
        }
        Console.WriteLine("\n[Program] Отсортированный список точек:");
        SegmentList = SegmentList.OrderBy(s => s.X).ThenBy(s => s.Y).ToList(); // Сортировка
        foreach (Segment segment in SegmentList) Console.WriteLine(segment.WriteSegment());

        List<Segment> minSegments = FindMinSegments(SegmentList);

        Console.WriteLine($"\n[Program] Минимальное количество отрезков: {minSegments.Count}");
        foreach (Segment segment in minSegments) Console.WriteLine(segment.WriteSegment());
        Console.ReadKey();
    }

    static List<Segment> FindMinSegments(List<Segment> segmentList)
    {
        List<Segment> minSegments = new List<Segment>();

        Segment currentSegment = segmentList.First();

        foreach (Segment segment in segmentList.Skip(1))
        {
            if (segment.X > currentSegment.Y)
            {
                // Текущий отрезок не покрывает текущую точку, добавляем новый отрезок
                minSegments.Add(currentSegment);
                currentSegment = segment;
            }
            else
            {
                // Обновляем текущий отрезок, если текущая точка входит в него
                currentSegment.Y = Math.Max(currentSegment.Y, segment.Y);
            }
        }

        // Добавляем последний отрезок
        minSegments.Add(currentSegment);

        return minSegments;
    }
}

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
