namespace HousingRepairsSchedulingApi.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

public class FileDataAttribute : DataAttribute
{
    private readonly string _filePath;


    /// <summary>
    ///     Load data from a file as the data source for a theory
    /// </summary>
    /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
    public FileDataAttribute(string filePath) => this._filePath = filePath;


    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

        var path = Path.IsPathRooted(this._filePath)
            ? this._filePath
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), this._filePath);

        if (!File.Exists(path))
        {
            throw new ArgumentException($"Could not find file at path: {path}");
        }

        // Load the file
        var fileData = File.ReadAllText(this._filePath);

        var objectList = new List<object[]> { new[] { fileData } };
        return objectList;
    }
}
