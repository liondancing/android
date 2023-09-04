using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBooking.Models
{
    internal class Note
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Note()
        {
            FileName = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
        }

        public void Save() => File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory), Text);

        public void Delete() => File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName));

        public static Note Load (string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Unable to find file on local storage!", filename);
            }
            return new()
            {
                FileName = Path.GetFileName(filename),
                Text = File.ReadAllText(filename),
                Date = File.GetLastAccessTime(filename)
            };
        }

        public static IEnumerable<Note> ReadAll()
        {
            // Get the folder where the notes are stored
            string appDataPath = FileSystem.AppDataDirectory;

            // Use link extentions to load the *.notes.txt files
            return Directory.
                // select filenames from the directory
                EnumerateFiles(appDataPath, "*.notes.txt").
                // load Nodes from each filename
                Select(filename => Note.Load(Path.GetFileName(filename))).
                // final collection of notes, order them by note
                OrderByDescending(note => note.Date);
        }
    }
}
