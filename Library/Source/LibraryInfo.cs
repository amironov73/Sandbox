using System;
using System.Diagnostics;

public static class LibraryInfo
{
    public static FileVersionInfo GetLibraryInfo()
    {
        var assembly = typeof (LibraryInfo).Assembly;
        var versionInfo = FileVersionInfo.GetVersionInfo (assembly.Location);
        return versionInfo;
    }
}
