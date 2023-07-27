// Задание 1

var (file1, file2, file3) = ("file1.txt", "file2.txt", "file3.txt");

CreateFiles();

Task task1 = Task.Run(() =>
{
    CountSpacesInFile(file1);
});
Task task2 = Task.Run(() =>
{
    CountSpacesInFile(file2);
});
Task task3 = Task.Run(() =>
{
    CountSpacesInFile(file3);
});

Task.WaitAll(task1, task2, task3);

Console.WriteLine("Finished");








void CountSpacesInFile(string s)
{
    var file1Text = File.ReadAllText(s);
    var spaceCount = file1Text.Count(it => it == ' ');
    Console.WriteLine($"File {s} contains {spaceCount} spaces");
}

void CreateFiles()
{
    
    var file1Content = "hello world";
    var file2Content = "hello great world";
    var file3Content = "hello funny great world";

    File.WriteAllText(file1, file1Content);
    File.WriteAllText(file2, file2Content);
    File.WriteAllText(file3, file3Content);
}
