using System;

public static class LibraryInfo
{
    public static Version? GetLibraryInfo()
    {
        return typeof (LibraryInfo).Assembly.GetName().Version;
    }
}
