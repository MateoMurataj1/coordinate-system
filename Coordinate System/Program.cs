
using System.Text.RegularExpressions;

Console.WriteLine("Enter the path of the text file: ");
string filePath = Console.ReadLine();   //example ---> string filePath = "C:\\Users\\User\\Desktop\\point.txt";

try
{
    if (File.Exists(filePath))
    {
        List<Point> points = new List<Point>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Match match = Regex.Match(line, @"Point\d+\(([\d\.\-]+), ([\d\.\-]+)\)");

                if (match.Success && match.Groups.Count == 3)
                {
                    string pointName = match.Groups[0].Value;
                    double x = double.Parse(match.Groups[1].Value);
                    double y = double.Parse(match.Groups[2].Value);
                    Console.Write($"{pointName}");
                    points.Add(new Point(x, y));

                    if (x > 0 && y > 0)
                    {
                        Console.WriteLine(" ---> Quadrant 1");
                    }
                    else if (x < 0 && y > 0)
                    {
                        Console.WriteLine(" ---> Quadrant 2");
                    }
                    else if (x < 0 && y < 0)
                    {
                        Console.WriteLine(" ---> Quadrant 3");
                    }
                    else if (x > 0 && y < 0)
                    {
                        Console.WriteLine(" ---> Quadrant 4");
                    }
                    else if (x == 0 && y == 0)
                    {
                        Console.WriteLine(" ---> Center");
                    }
                    else if (x == 0)
                    {
                        Console.WriteLine(" ---> On Y-axis");
                    }
                    else
                    {
                        Console.WriteLine(" ---> On X-axis");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid line: {line}");
                }
            }
        }

        Point furthestPoint = FurthestPoint(points);

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Match match = Regex.Match(line, @"Point\d+\(([\d\.\-]+), ([\d\.\-]+)\)");
                if (match.Success && match.Groups.Count == 3)
                {
                    string pointName = match.Groups[0].Value;
                    double x = double.Parse(match.Groups[1].Value);
                    double y = double.Parse(match.Groups[2].Value);

                    if (x == furthestPoint.X && y == furthestPoint.Y)
                    {
                        Console.WriteLine($"The furthest point from the center is {pointName}");
                    }
                }
            }
        }
    }
    else
    {
        Console.WriteLine("File not found");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static Point FurthestPoint(List<Point> points)
{
    Point furthestPoint = null;
    double maxDistance = double.MinValue;

    foreach (Point point in points)
    {
        double distance = Math.Sqrt(((point.X - 0) * (point.X - 0)) + ((point.Y - 0) * (point.Y - 0)));
        if (distance > maxDistance)
        {
            maxDistance = distance;
            furthestPoint = point;
        }
    }
    return furthestPoint;
}

class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}