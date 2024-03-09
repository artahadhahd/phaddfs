namespace Parser;

readonly struct Parser(string path)
{
    public readonly string Path = path;

    public readonly List<Tuple<float, float>>? Parse()
    {
        if (!File.Exists(Path))
        {
            try
            {
                File.Create(Path);
            } catch {
                Console.WriteLine($"File {Path} doesn't exist and I failed to create it");
                return null;
            }
            Console.WriteLine($"[!] Created file {Path}, it didn't exist.");
            return null;
        }
        return null;
    }
}