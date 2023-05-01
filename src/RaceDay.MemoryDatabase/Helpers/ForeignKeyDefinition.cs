using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.Helpers;

public class ForeignKeyDefinition
{
    #region Properties

    public string Name => $"fk{ParentTableName}_{ChildTableName}";
    public TableName ParentTableName { get; }
    public string[] ParentColumnNames { get; }

    public TableName ChildTableName { get; }
    public string[] ChildColumnNames { get; }

    #endregion

    #region Constructors

    public ForeignKeyDefinition(TableName parentTableName, string[] parentColumnNames, TableName childTableName, string[] childColumnNames)
    {
        if (parentColumnNames.Length != childColumnNames.Length)
            throw new ArgumentException("Parent and child column names must be the same length.");

        if (parentTableName == childTableName)
            throw new ArgumentException("Parent and child table names must be different.");

        ParentTableName = parentTableName;
        ParentColumnNames = parentColumnNames;
        ChildTableName = childTableName;
        ChildColumnNames = childColumnNames;
    }

    #endregion
}