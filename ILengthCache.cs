namespace MediaHelpers.MediaLengthCoreLibrary;
public interface ILengthCache
{
    Task<int?> GetLengthAsync(string path); //null means not found.
    DateOnly EarliestDateToUse { get; } //so anything before can attempt this list.
}