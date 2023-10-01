using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using data.models;

namespace data;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public string DbPath { get; set; }

    public UserContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        
}
