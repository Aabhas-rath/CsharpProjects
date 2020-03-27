namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.CGVSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Repository.CGVSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Database.ExecuteSqlCommand(@"USE [CGVS];
                                                SET IDENTITY_INSERT [dbo].[Albums] ON ;
                                                INSERT [dbo].[Albums] ([Id], [Name], [FolderPath]) VALUES (1, N'System', N'\Content\Images');
                                                INSERT [dbo].[Albums] ([Id], [Name], [FolderPath]) VALUES (2, N'WebStore', N'\Content\Images\WebStore');
                                                SET IDENTITY_INSERT [dbo].[Albums] OFF;
                                                SET IDENTITY_INSERT [dbo].[Images] ON ;
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (1, N'\Content\Images\Aabhas.jpg', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (2, N'\Content\Images\logo.png', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (3, N'\Content\Images\icon.png', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (4, N'\Content\Images\landingPageImage.jpg', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (5, N'\Content\Images\landingPageImage1.png', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (6, N'\Content\Images\aboutUs.jpg', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (7, N'\Content\Images\clockBckgrnd.png', 1, 1);
                                                INSERT [dbo].[Images] ([id], [Path], [Version], [AlbumId]) VALUES (9, N'\Content\Images\Physics.jpg', 1, 1);
                                                SET IDENTITY_INSERT [dbo].[Images] OFF;
                                                ");
        }
    }
}
