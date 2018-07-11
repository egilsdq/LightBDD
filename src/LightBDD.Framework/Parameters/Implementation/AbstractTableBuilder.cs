﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LightBDD.Framework.Parameters.Implementation
{
    [DebuggerStepThrough]
    internal abstract class AbstractTableBuilder<TRow, TColumn> where TColumn : InputTableColumn
    {
        private readonly List<TColumn> _customColumns = new List<TColumn>();
        protected bool InferColumns { get; set; }

        protected IEnumerable<TColumn> BuildColumns(TRow[] rows)
        {
            if (!InferColumns)
                return _customColumns;

            var custom = _customColumns.ToList();

            TColumn FindCustom(string name)
            {
                var index = custom.FindIndex(x => x.Name == name);
                if (index < 0)
                    return null;
                var column = custom[index];
                custom.RemoveAt(index);
                return column;
            }

            var results = TableColumnProvider.InferColumns(rows, true)
                .Select(CreateColumn)
                .Select(column => FindCustom(column.Name) ?? column)
                .ToList();
            results.AddRange(custom);
            return results;
        }

        protected void AddCustomColumn(TColumn column)
        {
            var currentIdx = _customColumns.FindIndex(c => c.Name == column.Name);

            if (currentIdx >= 0)
                _customColumns[currentIdx] = column;
            else
                _customColumns.Add(column);
        }

        protected abstract TColumn CreateColumn(ColumnInfo columnInfo);
    }
}