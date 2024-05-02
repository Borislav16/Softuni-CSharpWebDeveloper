namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {



            MusicHubDbContext context =
                new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            //Test your solutions here
            //Console.WriteLine(ExportAlbumsInfo(context, 9));

            Console.WriteLine(ExportSongsAboveDuration(context, 4));

        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {

            var albumsInfo = context.Producers
                .Include(p => p.Albums)
                .ThenInclude(a => a.Songs)
                .ThenInclude(s => s.Writer)
                .First(p => p.Id == producerId)
                .Albums.Select(a => new
                {
                    AlbumName = a.Name,
                    a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    AlbumSongs = a.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        SongPrice = s.Price,
                        SongWriterName = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.SongWriterName),
                    TotalAlbumPrice = a.Price,

                })
                .OrderByDescending(s => s.TotalAlbumPrice)
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var album in albumsInfo)
            {
                stringBuilder.AppendLine($"-AlbumName: {album.AlbumName}");
                stringBuilder.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                stringBuilder.AppendLine($"-ProducerName: {album.ProducerName}");
                stringBuilder.AppendLine($"-Songs:");

                int count = 1;
                foreach (var song in album.AlbumSongs)
                {
                    stringBuilder.AppendLine($"---#{count++}");
                    stringBuilder.AppendLine($"---SongName: {song.SongName}");
                    stringBuilder.AppendLine($"---Price: {song.SongPrice:F2}");
                    stringBuilder.AppendLine($"---Writer: {song.SongWriterName}");

                }

                stringBuilder.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:F2}");
            }

            return stringBuilder.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {

            var songsAboveDuration = context.Songs
                .Include(s => s.Writer)
                .Include(s => s.SongPerformers)
                .ThenInclude(sp => sp.Performer)
                .Include(s => s.Album)
                .ThenInclude(s => s.Producer)
                .ToList()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    s.Name,
                    SongPerformerFullName = s.SongPerformers.Select(sp => new
                    {
                        FullName = sp.Performer.FirstName + " " + sp.Performer.LastName,
                    }).OrderBy(P => P.FullName).ToList(),
                    SongWriterName = s.Writer.Name,
                    SongAlbumProducer = s.Album.Producer.Name,
                    SongDuration = s.Duration.ToString("c"),
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.SongWriterName)
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            int count = 1;
            foreach (var song in songsAboveDuration)
            {
                stringBuilder.AppendLine($"-Song #{count++}");
                stringBuilder.AppendLine($"---SongName: {song.Name}");
                stringBuilder.AppendLine($"---Writer: {song.SongWriterName}");
                if (song.SongPerformerFullName.Any())
                {
                    foreach (var performer in song.SongPerformerFullName)
                    {
                        stringBuilder.AppendLine($"---Performer: {performer.FullName}");
                    }
                }
                stringBuilder.AppendLine($"---AlbumProducer: {song.SongAlbumProducer}");
                stringBuilder.AppendLine($"---Duration: {song.SongDuration}");

            }
            
            return stringBuilder.ToString().Trim();
        }
    }
}
