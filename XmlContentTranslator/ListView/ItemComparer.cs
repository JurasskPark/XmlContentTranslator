using System.Collections;

namespace MES.Shared
{
    /// <summary>
    /// ListView item comparer.
    /// <para>Компаратор элементов ListView.</para>
    /// </summary>
    public class ItemComparer : IComparer
    {
        #region Variable

        private int columnIndex;
        private bool sortAscending = true;

        #endregion Variable

        #region Property

        /// <summary>
        /// Sets the sorted column index and toggles sorting direction.
        /// <para>Задает индекс сортируемого столбца и переключает направление сортировки.</para>
        /// </summary>
        public int ColumnIndex
        {
            set
            {
                if (columnIndex == value)
                {
                    sortAscending = !sortAscending;
                }
                else
                {
                    columnIndex = value;
                    sortAscending = true;
                }
            }
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Compares two ListView items.
        /// <para>Сравнивает два элемента ListView.</para>
        /// </summary>
        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem left || y is not ListViewItem right)
            {
                return 0;
            }

            string value1 = left.SubItems[columnIndex].Text;
            string value2 = right.SubItems[columnIndex].Text;
            return string.Compare(value1, value2, StringComparison.Ordinal) * (sortAscending ? 1 : -1);
        }

        #endregion Basic
    }
}
