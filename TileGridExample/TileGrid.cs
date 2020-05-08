using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace TileGridExample
{

    [ContentProperty("Children")]
    public class TileGrid : Control
    {
        private Grid _grid;


        public override void OnApplyTemplate()
        {
            _grid = GetTemplateChild("GRID") as Grid;


            //_collection = _grid.Children;
            _grid.Children.RemoveRange(0, _grid.Children.Count - 1);
            foreach (UIElement child in Children)
            {
                FrameworkElement fe = (FrameworkElement)ItemTemplate.LoadContent();
                fe.DataContext = child;
                Grid.SetRow(fe, TileGrid.GetRow(child));
                Grid.SetRowSpan(fe, TileGrid.GetRowSpan(child));
                Grid.SetColumn(fe, TileGrid.GetColumn(child));
                Grid.SetColumnSpan(fe, TileGrid.GetColumnSpan(child));
                _grid.Children.Add(fe);
            }
            SetRow(RowCount);
            SetColumn(ColumnCount);
        }

        public TileGrid() : base()
        {
            Collection<UIElement> collection = new Collection<UIElement>();
            SetValue(ChildrenProperty, collection);
        }

        static TileGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TileGrid), new FrameworkPropertyMetadata(typeof(TileGrid)));
        }

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register("Gap",
            typeof(Int32), typeof(TileGrid), new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.AffectsRender), ValidateValue);

        public static readonly DependencyProperty RowCountProperty = DependencyProperty.Register("RowCount",
            typeof(Int32), typeof(TileGrid), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender, RowCountChanged), ValidateValue);

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount",
            typeof(Int32), typeof(TileGrid), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender, ColumnCountChanged), ValidateValue);

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register("Children",
            typeof(Collection<UIElement>), typeof(TileGrid), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, ContentChanged));

        private static void ContentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void RowCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TileGrid tileGrid = dependencyObject as TileGrid;
            if (tileGrid._grid == null) return;
            int oldValue, newValue;
            if (Int32.TryParse(e.NewValue.ToString(), out newValue))
            {

                oldValue = tileGrid._grid.RowDefinitions.Count;

                if (tileGrid != null)
                {
                    if (newValue > oldValue)
                    {
                        for (int i = 0; i < newValue - oldValue; i++)
                            tileGrid._grid.RowDefinitions.Add(new RowDefinition());
                    }
                    else if (newValue < oldValue)
                    {
                        tileGrid._grid.RowDefinitions.RemoveRange(newValue, oldValue - newValue);
                    }
                }
            }
        }

        private void SetRow(int rowCount)
        {
            var oldValue = _grid.RowDefinitions.Count;

            if (rowCount > oldValue)
            {
                for (int i = 0; i < rowCount - oldValue; i++)
                    _grid.RowDefinitions.Add(new RowDefinition());
            }
            else if (rowCount < oldValue)
            {
                _grid.RowDefinitions.RemoveRange(rowCount, oldValue - rowCount);
            }

        }

        private void SetColumn(int columnCount)
        {
            var oldValue = _grid.ColumnDefinitions.Count;

            if (columnCount > oldValue)
            {
                for (int i = 0; i < columnCount - oldValue; i++)
                    _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            else if (columnCount < oldValue)
            {
                _grid.ColumnDefinitions.RemoveRange(columnCount, oldValue - columnCount);
            }

        }

        private static void ColumnCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TileGrid tileGrid = dependencyObject as TileGrid;
            if (tileGrid._grid == null) return;
            int oldValue, newValue;
            if (Int32.TryParse(e.NewValue.ToString(), out newValue))
            {
                oldValue = tileGrid._grid.ColumnDefinitions.Count;

                if (tileGrid != null)
                {
                    if (newValue > oldValue)
                    {
                        for (int i = 0; i < newValue - oldValue; i++)
                            tileGrid._grid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    else if (newValue < oldValue)
                    {
                        tileGrid._grid.ColumnDefinitions.RemoveRange(newValue, oldValue - newValue);
                    }
                }
            }
        }

        private static bool ValidateValue(object value)
        {
            int currentValue = (int)value;
            if (currentValue >= 0) // если текущее значение от нуля и выше
                return true;
            return false;
        }

        public int Gap
        {
            get { return (int)GetValue(GapProperty); }
            set { SetValue(GapProperty, value); }
        }


        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public Collection<UIElement> Children
        {
            get { return (Collection<UIElement>)GetValue(ChildrenProperty); }
        }


        public static readonly DependencyProperty RowProperty = DependencyProperty.RegisterAttached(
          "Row",
          typeof(int),
          typeof(TileGrid),
          new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        public static void SetRow(UIElement element, int value)
        {
            element.SetValue(RowProperty, value);
        }
        public static int GetRow(UIElement element)
        {
            return (int)element.GetValue(RowProperty);
        }

        public static readonly DependencyProperty RowSpanProperty = DependencyProperty.RegisterAttached(
          "RowSpan",
          typeof(int),
          typeof(TileGrid),
          new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        public static void SetRowSpan(UIElement element, int value)
        {
            element.SetValue(RowSpanProperty, value);
        }
        public static int GetRowSpan(UIElement element)
        {
            return (int)element.GetValue(RowSpanProperty);
        }

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached(
          "Column",
          typeof(int),
          typeof(TileGrid),
          new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        public static void SetColumn(UIElement element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }
        public static int GetColumn(UIElement element)
        {
            return (int)element.GetValue(ColumnProperty);
        }

        public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached(
          "ColumnSpan",
          typeof(int),
          typeof(TileGrid),
          new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        public static void SetColumnSpan(UIElement element, int value)
        {
            element.SetValue(ColumnSpanProperty, value);
        }
        public static int GetColumnSpan(UIElement element)
        {
            return (int)element.GetValue(ColumnSpanProperty);
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate",
            typeof(DataTemplate), typeof(TileGrid), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


    }
}
