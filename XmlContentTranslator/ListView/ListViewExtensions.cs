namespace MES.Shared
{
    /// <summary>
    /// ListView movement directions.
    /// <para>Направления перемещения ListView.</para>
    /// </summary>
    public enum MoveDirection
    {
        Up = -1,
        Down = 1
    }

    /// <summary>
    /// Extensions for ListView.
    /// <para>Расширения для ListView.</para>
    /// </summary>
    public static class ListViewExtensions
    {
        #region Basic

        /// <summary>
        /// Moves selected rows up or down.
        /// <para>Перемещает выбранные строки вверх или вниз.</para>
        /// </summary>
        public static void MoveListViewItems(this ListView sender, MoveDirection direction)
        {
            int dir = (int)direction;

            bool valid = sender.SelectedItems.Count > 0 &&
                         ((direction == MoveDirection.Down &&
                           sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1) ||
                          (direction == MoveDirection.Up &&
                           sender.SelectedItems[0].Index > 0));

            if (!valid)
            {
                return;
            }

            sender.SuspendLayout();

            try
            {
                foreach (ListViewItem item in sender.SelectedItems)
                {
                    int index = item.Index + dir;
                    sender.Items.RemoveAt(item.Index);
                    sender.Items.Insert(index, item);
                    sender.Items[index].Selected = true;
                    sender.Focus();
                }
            }
            finally
            {
                sender.ResumeLayout();
            }
        }

        /// <summary>
        /// Resizes a column by its name.
        /// <para>Изменяет размер столбца по его имени.</para>
        /// </summary>
        public static void ResizeColumn(ListView listView, string columnName)
        {
            int index = GetColumnIndexByName(listView, columnName);

            listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView.Columns[index].Width += 20;
        }

        /// <summary>
        /// Gets a column index by its name.
        /// <para>Получает индекс столбца по его имени.</para>
        /// </summary>
        public static int GetColumnIndexByName(ListView listView, string columnName)
        {
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (listView.Columns[i].Text == columnName)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion Basic
    }
}
