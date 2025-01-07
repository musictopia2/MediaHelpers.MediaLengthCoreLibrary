namespace MediaHelpers.MediaLengthCoreLibrary;
public static class LengthClass
{
    public static ILengthCache? LengthCache { get; set; }
    public static async Task<int> LengthAsync(string tempPath)
    {
        // Check if the file exists
        if (File.Exists(tempPath))
        {
            FileInfo temp = new (tempPath);
            DateTime firsts = temp.LastWriteTime;
            DateOnly lastWriteDate = DateOnly.FromDateTime(firsts);
            try
            {
                if (LengthCache is not null)
                {
                    DateOnly earliestCacheDate = LengthCache.EarliestDateToUse;
                    if (lastWriteDate < earliestCacheDate)
                    {
                        int? value;
                        value = await LengthCache.GetLengthAsync(tempPath);
                        if (value is not null)
                        {
                            return value.Value;
                        }
                    }                       
                }
                // Load the media file using TagLibSharp
                var file = TagLib.File.Create(tempPath);

                // Get the duration of the media file
                var duration = file.Properties.Duration;

                // Return the length in seconds
                return (int)duration.TotalSeconds;
            }
            catch (Exception ex)
            {
                // Handle any errors, such as unsupported file formats
                Console.WriteLine($"Error reading media file: {ex.Message}");
            }
        }
        // Return -1 if file does not exist or error occurs
        return -1;
    }
}