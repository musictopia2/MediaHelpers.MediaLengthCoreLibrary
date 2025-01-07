namespace MediaHelpers.MediaLengthCoreLibrary;
public interface ILengthCache
{
    int? GetLength(string path); //null means not found.
    DateOnly EarliestDateToUse { get; } //so anything before can attempt this list.
}