﻿//using Microsoft.Maui.Storage;

namespace ShoppingOrganizer.Database;
internal class Constants
{
    public const string DatabaseFilename = "ShoppingMobileDatabase.db3";
    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine("dasd", DatabaseFilename); // TODO dat normalnu
}
