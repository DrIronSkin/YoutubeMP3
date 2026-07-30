using System;

namespace YoutubeMP3.Models;

public record Video(
    string Title,
    string Author,
    string Url,
    TimeSpan Duration);