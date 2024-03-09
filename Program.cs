using Raylib_cs;
namespace Program;

class Program
{
    const int width = 800, height = 500;

    public static void Main()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.None);
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(width, height, "Kräfte addieren");

        Visualizer.Visualizer visualizer = new();
        visualizer.AddForce(50, 0);
        visualizer.AddForce(10, 90);
        visualizer.AddForce(20, 45);
        //visualizer.AddForce(100, 100f);
        visualizer.Thickness = 3;

        Raylib.SetTargetFPS(30);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            visualizer.Render();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}