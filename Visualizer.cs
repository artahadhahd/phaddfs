using Raylib_CsLo;
using System.Numerics;
using Newton = float;
using Degrees = float;
using System.Data;

namespace Visualizer;

enum SystemDirection : int
{
    CW = 1,
    CCW = -1,
}

class Visualizer
{
    public SystemDirection Direction;
    private readonly List<Newton> _forces = [];
    private readonly List<Degrees> _angles = [];
    private float _thickness = 5f;

    public float Thickness
    {
        //get => _thickness;
        set => _thickness = value;
    }

    public Visualizer(SystemDirection direction)
    {
        Direction = direction;
    }

    public Visualizer()
    {
        Direction = SystemDirection.CCW;
    }

    public void AddForce(Newton force, Degrees angle)
    {
        _forces.Add(force);
        _angles.Add(angle);
    }

    public void Render()
    {
        if (_forces.Count != _angles.Count) { return; }
        Vector2 center = new(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        Vector2 sum = new();
        Vector2 origin = new();
        for (int i = 0; i < _angles.Count; i++)
        {
            var force = _forces[i];
            var angle = _angles[i];
            var x = (float)(Math.Cos(angle * Math.PI / 180) * force);
            var y = (float)(Math.Sin(angle * Math.PI / 180) * force);
            sum.X += x;
            sum.Y += y;
            Rectangle rect = new(center.X, center.Y, force, _thickness);
            Raylib.DrawRectanglePro(rect, origin, angle * (int)Direction, Raylib.ORANGE);
        }
        float fRes = Vector2.Distance(sum, origin);
        Rectangle fResVec = new(center.X, center.Y, fRes, _thickness);
        float resultAngle = (float)(Math.Atan2(sum.Y, sum.X) * (180 / Math.PI));
        Raylib.DrawRectanglePro(fResVec, origin, resultAngle * (int)Direction, Raylib.MAGENTA);

        string max = fRes.ToString();
        Rectangle resultantRect = new(Raylib.GetScreenWidth() - max.Length - 100, 5, 90, 30);
        RayGui.GuiTextBox(resultantRect, $"{max}", 10, false);
    }

    public void RmLast()
    {
        // angles and forces have the same length.
        var idx = _angles.Count - 1;
        if (idx >= 0)
        {
            _forces.RemoveAt(idx);
            _angles.RemoveAt(idx);
        }
    }
}
