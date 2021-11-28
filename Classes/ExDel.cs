using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace ExtensionDeleter
{
    public class ExDel
    {

        [Description("Delete all files with a given extension starting from a given folder")]
        public void DeleteExtensions(string startFolder, string extension, bool recursive) {

            string normalisedExtension = extension.Replace(".", "").ToLower();

            Console.WriteLine($"Starting Folder: {startFolder}");
            Console.WriteLine($"Extension: .{normalisedExtension}");
            Console.WriteLine($"Recursive: {recursive}");

            // First of all - get a list of all files with the extension
            IEnumerable<string> folders = GetAllFolders(startFolder, recursive);

            // Now that we have all folders - find the list of files with the given extension
            IEnumerable<string> files = GetAllFiles(folders, normalisedExtension);

            // Now Delete all of these files
            if (files.Count() > 0) {
                int deletedFileNo = DeleteFiles(files);
                Console.WriteLine($"Deleted {deletedFileNo} files");
            } else {
                Console.WriteLine($"No Files with extension {normalisedExtension} found - quitting...");
            }
        }

        [Description("Find All folders from a given start location")]
        private IEnumerable<string> GetAllFolders(string startFolder, bool recursive) {

            IEnumerable<string> rawFolders;

            if (recursive) {
                rawFolders = Directory.EnumerateDirectories(startFolder, "*", SearchOption.AllDirectories);
            } else {
                rawFolders = Directory.EnumerateDirectories(startFolder, "*");
            }

            // foreach (var folder in rawFolders)
            // {
            // // Console.WriteLine($"Folder Found: {folder}");
            // }

            Console.WriteLine($"{rawFolders.Count()} folders found.");
            return rawFolders;

        }

        [Description("Get all files from the given folders with the given extension")]
        private IEnumerable<string> GetAllFiles(IEnumerable<string> folders, string extension) {
            IEnumerable<string> rawFiles;
            List<string> files = new List<string>();

            foreach (var folder in folders)
            {
                rawFiles = Directory.EnumerateFiles(folder, $"*.{extension}");

                foreach (var file in rawFiles)
                {
                    // System.Console.WriteLine(file);
                    files.Add(file);
                }
            }

            Console.WriteLine($"{files.Count()} files with .{extension} extension found.");
            return files;
        }

        private int DeleteFiles(IEnumerable<string> files) {
            int deletedFiles = 0;
            

            foreach (var file in files)
            {
                try {
                    File.Delete(file);
                    deletedFiles++;

                } catch (Exception error) {
                    Console.WriteLine($"File: {file} - not deleted due to exception: {error}");
                }
            }

            return deletedFiles;
        }
    }
}