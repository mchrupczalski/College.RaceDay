using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

public abstract class TableDefinitionBase<TEntity>
    where TEntity : class
{
    #region Fields

    private readonly TableName _tableName;

    private DataTable? _table;

    #endregion

    #region Properties

    protected string DataFilesRoot { get; }
    protected string DataFileName { get; }

    public DataTable Table
    {
        get
        {
            if (_table == null) Create();
            Debug.Assert(_table != null, nameof(_table) + " != null");
            return _table;
        }
    }

    #endregion

    #region Constructors

    protected TableDefinitionBase(TableName tableName, string dataFilesRoot, string dataFileName)
    {
        DataFilesRoot = dataFilesRoot;
        DataFileName = dataFileName;
        _tableName = tableName;
    }

    #endregion

    #region Abstract Members

    protected abstract DataColumn[] GetPrimaryKeyColumns();

    #endregion

    private void Create()
    {
        _table = new DataTable(_tableName.ToString());
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            _table.Columns.Add(CreateColumn(property.Name));
        }

        _table.PrimaryKey = GetPrimaryKeyColumns();
    }

    private static DataColumn CreateColumn(string propertyName, bool allowNull = false)
    {
        var propertyInfo = typeof(TEntity).GetProperty(propertyName) ?? throw new InvalidOperationException("Property not found.");
        var propertyType = propertyInfo.PropertyType;

        if (!propertyType.IsPrimitive && propertyType != typeof(string) && propertyType != typeof(DateTime))
            throw new InvalidOperationException($"Property {propertyType} type of {propertyName} in {typeof(TEntity)} not supported.");

        var column = new DataColumn(propertyName, propertyType);
        column.AllowDBNull = allowNull;
        return column;
    }

    public IEnumerable<TEntity> GetEntities()
    {
        var output = new List<TEntity>();

        foreach (DataRow row in Table.Rows)
        {
            var entity = Activator.CreateInstance<TEntity>();
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entity, row[property.Name]);
            }

            output.Add(entity);
        }

        return output;
    }

    public void AddEntity(TEntity entity)
    {
        var row = Table.NewRow();
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            var column = Table.Columns[property.Name] ?? throw new InvalidOperationException("Column not found.");
            if(column.AutoIncrement) continue;
            
            row[property.Name] = property.GetValue(entity);
        }

        Table.Rows.Add(row);
        SaveDataToFile();
    }

    public void LoadDataFromFile()
    {
        string filePath = Path.Combine(DataFilesRoot, DataFileName);
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);
        var entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json)
                                 ?.ToArray() ?? Array.Empty<TEntity>();
        foreach (var entity in entities)
        {
            AddEntity(entity);
        }
    }

    public void SaveDataToFile()
    {
        string filePath = Path.Combine(DataFilesRoot, DataFileName);
        var entities = GetEntities()
           .ToArray();
        string json = JsonConvert.SerializeObject(entities, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}