using Raylib_CsLo;
using System.Globalization;
namespace Program;

// could also put the rest of the gui here but i couldn't care less
struct Gui()
{
    private float _iota = 0;

    public float Iota()
    {
        var iota = _iota;
        _iota += 30;
        return iota;
    }
}

class Program
{
    const int width = 800, height = 500;
    const float leftAlign = 10f;

    public static void Main()
    {
        Console.WriteLine("This is open source software. Source code available at https://github.com/artahadhahd/phaddfs");

        Raylib.SetTraceLogLevel((int)TraceLogLevel.LOG_NONE);
        Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(width, height, "Add Forces");

        Gui gui = new();

        Visualizer.Visualizer visualizer = new();
        visualizer.Thickness = 3;

        Rectangle ccwToggle = new(leftAlign, gui.Iota(), 40, 20);
        bool toggleCcw = true;
        Rectangle addForceRect = new(leftAlign, gui.Iota(), 200, 20);
        string addForceText = "";
        Rectangle addAngleRect = new(leftAlign, gui.Iota(), 200, 20);
        string addAngleText = "";
        Rectangle submitRect = new(leftAlign, gui.Iota(), 30, 20);
        Rectangle removeLastRect = new(leftAlign, gui.Iota(), 70, 20);

        Raylib.SetTargetFPS(50);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);
            toggleCcw = RayGui.GuiToggle(ccwToggle, toggleCcw ? "CCW" : "CW", toggleCcw);
            visualizer.Direction = toggleCcw ? Visualizer.SystemDirection.CCW : Visualizer.SystemDirection.CW;

            // code here is repeated twice, i just didn't have time to create another abstraction
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), addForceRect))
            {
                int key = Raylib.GetCharPressed();
                if (key >= '0' && key <= '9' || key == '.')
                {
                    addForceText += (char)key;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    var idx = addForceText.Length - 1;
                    if (idx >= 0) {
                        addForceText = addForceText.Remove(idx);
                    }
                }
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), addAngleRect))
            {
                int key = Raylib.GetCharPressed();
                if (key >= '0' && key <= '9' || key == '.')
                {
                    addAngleText += (char)key;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    var idx = addAngleText.Length - 1;
                    if (idx >= 0)
                    {
                        addAngleText = addAngleText.Remove(idx);
                    }
                }
            }

            RayGui.GuiTextBox(addForceRect, $"Add force: {addForceText}", 30, true);
            RayGui.GuiTextBox(addAngleRect, $"Add angle: {addAngleText}", 30, true);

            if (RayGui.GuiButton(submitRect, "Add"))
            {
                if (addForceText != "" && addAngleText != "")
                {
                    var force = float.Parse(addForceText, CultureInfo.InvariantCulture);
                    var angle = float.Parse(addAngleText, CultureInfo.InvariantCulture);
                    visualizer.AddForce(force, angle);
                    addForceText = addAngleText = "";
                }
            }

            if (RayGui.GuiButton(removeLastRect, "Remove last"))
            {
                visualizer.RmLast();
            }

            visualizer.Render();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}