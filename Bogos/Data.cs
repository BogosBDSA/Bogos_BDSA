using System.Collections;
using System.Globalization;
using CsvHelper;


namespace Bogos;

public record Data(int? ID, string Author, int? Commit, string Date, string Message);

public class DataCollection : IEnumerable<Data>
{
    private DataCollection() {}

    public static DataCollection Create() => new DataCollection();

    private static Lazy<IReadOnlyCollection<Data>> Read { get; } = new Lazy<IReadOnlyCollection<Data>>(() =>
    {
        var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../DataFolder/Information.csv");
        var csv = File.OpenText(file);
        using var reader = new CsvReader(csv, CultureInfo.InvariantCulture);
        return reader.GetRecords<Data>().ToList().AsReadOnly();
    });

    public IEnumerator<Data> GetEnumerator() => Read.Value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
